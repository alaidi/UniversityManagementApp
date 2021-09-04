using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.DataLayer;
using UniversityManagement.Entities;
using UniversityManagement.Services;

namespace UniversityManagement.BusinessLayer
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly DataAccess da;
        private readonly string _tableName;

        public GenericRepository()
        {
            da = new DataAccess();
            _tableName = typeof(T).Name;
        }
        public List<T> GetData()
        {
            var sql = $"Select * from {_tableName}";
            var dataTable = da.GetMultipleItem(sql);
            return ConvertData.ConvertDataTable<T>(dataTable);
        }

        public int Add(T entity)
        {
            var param = new List<SqlParameter>();
            var sql =new StringBuilder($"insert into {_tableName} (");
            //   sql.Append("name,id) values(@Name,@Id)");
            var properties = ConvertData.GenerateListOfProperties<T>(entity);

            foreach (var property in properties)
            {
                if (property.Name!="Id") 
                    sql.Append($"[{property.Name}],");
            }

            sql.Remove(sql.Length - 1, 1).Append(") values( ");
            foreach (var property in properties)
            {
                if (property.Name != "Id")
                {
                    sql.Append($"@{property.Name},");
                    param.Add(new SqlParameter
                    {
                        ParameterName = $"@{property.Name}",
                        Value = property.Value
                    });
                }
            }
            sql.Remove(sql.Length - 1, 1).Append(")");
            return da.InsertDeleteUpdate(sql.ToString(),param.ToArray());
        }

        public int Update(T entity)
        {
            var param = new List<SqlParameter>();
            var sql = new StringBuilder($"UPDATE {_tableName} set ");
            var properties = ConvertData.GenerateListOfProperties<T>(entity);
            foreach (var propertyAndValue in properties)
            {
                if (propertyAndValue.Name != "Id")
                    sql.Append($"{propertyAndValue.Name}=@{propertyAndValue.Name},");
                param.Add(new SqlParameter
                {
                    ParameterName = $"@{propertyAndValue.Name}",
                    Value = propertyAndValue.Value
                });
            }
            sql.Remove(sql.Length - 1, 1).Append(" WHERE Id=@Id");

            return da.InsertDeleteUpdate(sql.ToString(), param.ToArray());
        }

        public int Delete(int id)
        {
            var sql = $"Delete from {_tableName} where Id=@Id";
            return da.InsertDeleteUpdate(sql, new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            });
        }
    }
}
