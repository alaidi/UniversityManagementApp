using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ClassLibrary1
{
    [Generator]
    public class JsonSerializerGenerator : ISourceGenerator
    {
        private const string AttributeText = @"
using System;
namespace Generated
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsonSerializableAttribute : Attribute
    {
        public JsonSerializableAttribute() { }
    }
}";
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("JsonSerializableAttribute", SourceText.From(AttributeText, Encoding.UTF8));

            if (!(context.SyntaxReceiver is MySyntaxReceiver receiver))
            {
                return;
            }

            CSharpParseOptions options = ((CSharpCompilation)context.Compilation).SyntaxTrees[0].Options as CSharpParseOptions;

            SyntaxTree attributeSyntaxTree = CSharpSyntaxTree.ParseText(SourceText.From(AttributeText, Encoding.UTF8), options);
            Compilation compilation = context.Compilation.AddSyntaxTrees(attributeSyntaxTree);

            INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName("Generated.JsonSerializableAttribute");
            INamedTypeSymbol stringSymbol = compilation.GetTypeByMetadataName("System.String");

            StringBuilder sb = new StringBuilder();
            sb.Append($@"
namespace MyProgram
{{
    public class GeneratedSerializer
    {{");

            foreach (var classDeclaration in receiver.CandidateClasses)
            {
                SemanticModel model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                ITypeSymbol classSymbol = ModelExtensions.GetDeclaredSymbol(model, classDeclaration) as ITypeSymbol;

                // Do not generate an overload if we do not find our attribute on the class in question
                if (!classSymbol.GetAttributes().Any(a => a.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
                {
                    continue;
                }

                sb.Append($@"public string Serialize({classDeclaration.Identifier.Text} input)
                {{");
                // return $"{{ \"Name\" : \"{thing.Name}\", \"Weight\" : {thing.Weight} }}";

                sb.Append("return $\"{{ ");

                List<PropertyDeclarationSyntax> propertyDeclarations = classDeclaration.Members.OfType<PropertyDeclarationSyntax>().ToList();
                for (int i = 0; i < propertyDeclarations.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }

                    var propertyDeclaration = propertyDeclarations[i];

                    IPropertySymbol propertySymbol = (IPropertySymbol) ModelExtensions.GetDeclaredSymbol(model, propertyDeclaration);
                    if (propertySymbol.Type.Equals(stringSymbol, SymbolEqualityComparer.Default))
                    {
                        sb.Append($"\\\"{propertyDeclaration.Identifier.Text}\\\" : \\\"{{input.{propertyDeclaration.Identifier.Text}}}\\\"");
                    }
                    else
                    {
                        sb.Append($"\\\"{propertyDeclaration.Identifier.Text}\\\" : {{input.{propertyDeclaration.Identifier.Text}}}");
                    }
                }
                sb.Append(" }}\";");

                sb.Append($" }}");
            }

            sb.Append($@"
    }}
}}");

            context.AddSource("GeneratedSerializer", SourceText.From(sb.ToString(), Encoding.UTF8));

        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }
    }

    class MySyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
                    classDeclarationSyntax.AttributeLists.Count > 0)
            {
                CandidateClasses.Add(classDeclarationSyntax);
            }
        }
    }
}
