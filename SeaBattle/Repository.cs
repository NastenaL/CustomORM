﻿namespace SeaBattle
{
    using SeaBattleBasic;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Reflection;
    using СustomORM;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected string TableName
        {
            get
            {
                return typeof(T).Name;
            }
        }

        public string GetById(int id)
        {
            string result = "";
            var query = $"select * from {TableName} where id = {id}";
            using (SqlConnection connection = new SqlConnection(Сonfiguration.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while(reader.Read())
                    {
                        result = "Ship #" + reader.GetInt32(0) + " was founded " +
                                  "  " + reader.GetInt32(1) +
                                  "  " + reader.GetInt32(2) +
                                  "  " + reader.GetInt32(3) +
                                  "  " + reader.GetInt32(4) + "\n";
                    }    
                }
                finally
                {
                    reader.Close();
                }
            }
            return result;
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

        public List<string> GetAll()
        {
            List<string> result = new List<string>();
            var query = $"select * from {TableName}";
            using (SqlConnection connection = new SqlConnection(Сonfiguration.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        result.Add("Ship #" + reader.GetInt32(0) + " was founded " +
                                  "  " + reader.GetInt32(1) +
                                  "  " + reader.GetInt32(2) +
                                  "  " + reader.GetInt32(3) +
                                  "  " + reader.GetInt32(4) + "\n");
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return result;
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
            using (SqlConnection connection = new SqlConnection(Сonfiguration.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
