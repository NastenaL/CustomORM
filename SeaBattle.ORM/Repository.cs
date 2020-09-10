namespace SeaBattle.ORM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
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

            string columnsValues = "";
            foreach (PropertyInfo property in fields)
            {
                columnsValues += property.Name + " = '" + GetPropValue(entity, property.Name) + "'" + ",";
            }
            columnsValues = columnsValues.Remove(columnsValues.Length - 1);

            var id = entity.Id;
            var updateQuery = $"update {TableName} set {columnsValues} where Id = {id}";
            ExecuteQuery(updateQuery);
        }

        public void Insert(T entity)
        {
            var fields = typeof(T).GetProperties();
       
            string columns = "";
            string values = "";
            foreach(PropertyInfo property in fields)
            {
                columns += property.Name + ",";
                values += "'" +GetPropValue(entity, property.Name)+"'" + ",";
            }
            columns = columns.Remove(columns.Length - 1);
            values = values.Remove(values.Length - 1);

            var insertQuery = $"insert into {TableName} ({columns}) values ({values})";
            ExecuteQuery(insertQuery);
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
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
