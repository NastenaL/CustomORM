namespace SeaBattle.ORM
{
    using System;
    using System.Collections;
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

        private T FindElement(int id, string query)
        {
            var entity = new T();

            var dataAdapter = new SqlDataAdapter(query, connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var item = Activator.CreateInstance<T>();
                var type = typeof(T);
                var ignoredProperties = GetIgnoredProperties<T>(entity);

                var properties = type.GetProperties();

                foreach (var propertyInfo in properties)
                {
                    object fieldValue = null;
                    string wrongProperty = ignoredProperties.Find(a => a == propertyInfo.Name);
                    if (string.IsNullOrEmpty(wrongProperty))
                    {
                        var fieldName = propertyInfo.Name;
                        fieldValue = dataRow[fieldName];
                        propertyInfo.SetValue(item, fieldValue, new object[] { });
                    }
                }

                entity = item;
            }
            dataAdapter.Dispose();
            return entity;
        }

        public T GetById(int id)
        {
            string query = $"select * from {TableName} where Id= " + id;
            var entity =  FindElement(id, query);

            var relatedAttributes = GetRelatedAttributes(entity);
            if(relatedAttributes.Count > 0)
            {
                foreach(RelatedAttribute attribute in relatedAttributes)
                {
                    string query1 = $"select * from {attribute.TableName} where {attribute.ColumnName}= " + id;
                    var refEntities = GetElements(query1, attribute.PropertyName);

                    var entityProperty = typeof(T).GetProperty(attribute.PropertyName);
                    entityProperty.SetValue(entity, refEntities, null);
                }
            }
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

        private IList GetElements(string query, string propertyName)
        {
            var entities = new List<T>();
            var item = Activator.CreateInstance<T>();
            var type = typeof(T);

            var properties = type.GetProperties();

            var entityProperty = typeof(T).GetProperty(propertyName);

            IList refEntities = (IList)Activator.CreateInstance(entityProperty.PropertyType);

            Dictionary<string, string> mappingProperties = WriteColumnMappings<List<T>>(entities);
            T resultEntity = new T();

            var dataAdapter = new SqlDataAdapter(query, connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var e = entityProperty.PropertyType.AssemblyQualifiedName;
                var refType = Type.GetType(e, true);
       

                if (refType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var itemType = refType.GetGenericArguments()[0];
                    var refProperties = itemType.GetProperties();

                    var entitiesList = Activator.CreateInstance(itemType);

                    var atrributes = WriteColumnMappings(entitiesList);

                    foreach (var propertyInfo in refProperties)
                    {
                        var attribute = atrributes.FirstOrDefault(a => a.Key == propertyInfo.Name);
                        var fieldName = attribute.Key == null ? propertyInfo.Name : attribute.Value;

                        if (mappingProperties.Count > 0)
                        {
                            var mappingProperty = mappingProperties.FirstOrDefault(t => t.Value == propertyInfo.Name);
                            if (mappingProperty.Key != null)
                            {
                                fieldName = mappingProperty.Key;
                            }
                        }

                        var fieldValue = dataRow[fieldName];

                        propertyInfo.SetValue(entitiesList, fieldValue);
                    }

                    refEntities.Add(entitiesList);
                }
            }
            dataAdapter.Dispose();
            return refEntities;
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

                var atrr = WriteColumnMappings<T>(item);

                foreach (var propertyInfo in type.GetProperties())
                {
                    var qwerty = atrr.FirstOrDefault(a => a.Key == propertyInfo.Name);
                    var fieldName = qwerty.Key == null? propertyInfo.Name : qwerty.Value;

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

        private List<RelatedAttribute> GetRelatedAttributes<T>(T item) where T : new()
        {
            List<RelatedAttribute> result = new List<RelatedAttribute>();
            var type = item.GetType();

            var properties = item.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);

                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(RelatedEntityAttribute));
                if (columnMapping != null)
                {
                    var mapsTo = columnMapping as RelatedEntityAttribute;
                    RelatedAttribute relatedAttribute = new RelatedAttribute();

                    relatedAttribute.ColumnName = mapsTo.ColumnName;
                    relatedAttribute.PropertyName = mapsTo.PropertyName;
                    relatedAttribute.TableName = mapsTo.TableName;
                    result.Add(relatedAttribute);
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
