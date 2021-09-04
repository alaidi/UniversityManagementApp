using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace UniversityManagement.Services
{
    public class ConvertData
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            var data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo property in temp.GetProperties())
                {
                    if(property.Name==column.ColumnName)
                        property.SetValue(obj,dr[column.ColumnName],null);
                }
            }

            return obj;
        }
        //public static List<(string Name, string Value)> GenerateListOfProperties<T>(T entity)
        //{
        //    var lp = new List<(string Name, string Value)>();
        //    Type temp = typeof(T);
        //    foreach (var item in temp.GetProperties())
        //    {
        //        var t = (item.Name, Value: item.GetValue(entity).ToString());
        //        lp.Add(t);
        //        //prp.Add(new PropertyWithValue
        //        //{
        //        //    Name = item.Name,
        //        //    Value = item.GetValue(entity).ToString()
        //        //});
        //    }

        //    return lp;
        //}
        public static List<PropertyWithValue> GenerateListOfProperties<T>(T entity)
        {
            var prp = new List<PropertyWithValue>();
            Type temp = typeof(T);
            foreach (var item in temp.GetProperties())
            {
                prp.Add(new PropertyWithValue
                {
                    Name = item.Name,
                    Value = item.GetValue(entity).ToString()
                });
            }

            return prp;
        }
    }

    public class PropertyWithValue  
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}