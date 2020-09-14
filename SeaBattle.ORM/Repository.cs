namespace SeaBattle.ORM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SeaBattle;Integrated Security=True";

        protected string TableName
        {
            get
            {
                return typeof(T).Name;
            }
        }

        public T GetById(int id)
        {
            var entity = new T();
         
            var dataAdapter = new SqlDataAdapter($"select * from {TableName} where Id= " + id, connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var item = Activator.CreateInstance<T>();
                var type = typeof(T);
                
                foreach (var propertyInfo in type.GetProperties())
                {
                    var fieldName = propertyInfo.Name;
                    var fieldValue = dataRow[fieldName];

                    propertyInfo.SetValue(item, fieldValue, new object[] { });
                }

                entity = item;
            }          
            dataAdapter.Dispose();
            return entity;
        }

        public void Delete(T entity)
        { 
            if (entity.Id == 0)
            {
                return;
            }
            var deleteQuery = $"delete from {TableName} where id = {entity.Id}";
            ExecuteQuery(deleteQuery);
        }

        public List<T> GetAll()
        {
            var entities = new List<T>();
            
            var dataAdapter = new SqlDataAdapter($"select * from {TableName}", connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var item = Activator.CreateInstance<T>();
                var type = typeof(T);

                foreach (var propertyInfo in type.GetProperties())
                {
                    var fieldName = propertyInfo.Name;
                    var fieldValue = dataRow[fieldName];

                    propertyInfo.SetValue(item, fieldValue, new object[] { });
                }

                entities.Add(item);
            }

            dataAdapter.Dispose();
            return entities;
        }

        public void Update(T entity)
        {
            var fields = typeof(T).GetProperties();
            Dictionary<string, string> mappingProperties = WriteColumnMappings<T>(entity);

            string columnsValues = "";
           List<string> columns = new List<string>();
            foreach(PropertyInfo field in fields)
            {
                columns.Add(field.Name);
            }
            foreach (KeyValuePair<string, string> keyValue in mappingProperties)
            {
                var oldPropertyValue = fields.FirstOrDefault(r => r.Name == keyValue.Key);
                var oldPropertyIndex = columns.FindIndex(a => a == oldPropertyValue.Name);
                columns[oldPropertyIndex] = keyValue.Value;
            }
            foreach (string column in columns)
            {
                var oldProp = mappingProperties.FirstOrDefault(a => a.Value == column);
                var prop = column;
                if(oldProp.Key != null)
                {
                    prop = oldProp.Key;
                }
                columnsValues += column + " = '" + GetPropValue(entity, prop) + "'" + ",";
            }
            columnsValues = columnsValues.Remove(columnsValues.Length - 1);

            var id = entity.Id;
            var updateQuery = $"update {TableName} set {columnsValues} where Id = {id}";
            ExecuteQuery(updateQuery);
        }

        public void Insert(T entity)
        {
            var fields = typeof(T).GetProperties();

            var ignoredProperties = GetIgnoredProperties<T>(entity);
          
            Dictionary<string, string> mappingProperties = WriteColumnMappings<T>(entity);

            string columns = "";
            string values = "";
            foreach(PropertyInfo property in fields)
            {
                string wrongProperty = ignoredProperties.Find(a => a == property.Name);
                if (string.IsNullOrEmpty(wrongProperty))
                {
                    columns += property.Name + ",";
                    values += "'" + GetPropValue(entity, property.Name) + "'" + ",";
                }
            }
            columns = columns.Remove(columns.Length - 1);
            values = values.Remove(values.Length - 1);

            if(mappingProperties.Count > 0)
            {
                foreach (KeyValuePair<string, string> property in mappingProperties)
                {
                    columns = columns.Replace(property.Key, property.Value);
                }
            }

            var insertQuery = $"insert into {TableName} ({columns}) values ({values})";
            ExecuteQuery(insertQuery);
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private List<string> GetIgnoredProperties<T>(T item) where T:new()
        {
            List<string> result = new List<string>();
            var type = item.GetType();

            var properties = item.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);

                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(IgnorePropertyAttribute));
                if (columnMapping != null)
                {
                    var mapsTo = columnMapping as IgnorePropertyAttribute;
                    result.Add(property.Name);

                }
            }
            return result;
        }
       
        private Dictionary<string,string> WriteColumnMappings<T>(T item) where T : new()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var type = item.GetType();

            var properties = item.GetType().GetProperties();
           
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
    
                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(RedefineColumnAttribute));
                if (columnMapping != null)
                {
                    var mapsTo = columnMapping as RedefineColumnAttribute;
                    result.Add(property.Name, mapsTo.newName);
                 
                }
            }
            return result;
        }

        private void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
    }
}
