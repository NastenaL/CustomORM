namespace SeaBattle
{
    using SeaBattleBasic;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
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

        public T GetById(int id)
        {
            List<object> r = new List<object>();
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
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            r.Add(reader[i]);
                        }
                    }    
                }
                finally
                {
                    command.Dispose();
                    reader.Close();
                }
            }
            object rdsc = r;
            return (T)rdsc;
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

        public List<object> GetAll()
        {
            var propertyLength = typeof(T).GetProperties().Length;
            List<object> entity = new List<object>();
            List<object> E = new List<object>();
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

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            
                            if (i % propertyLength - 1 == 0)
                            {
                                E.Add(entity);
                                entity = new List<object>();
                            }
                            entity.Add(reader[i]);

                        }
                       
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return entity;
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
