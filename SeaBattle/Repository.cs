namespace SeaBattle
{
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using СustomORM;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SeaBattle;Integrated Security=True";

        protected string TableName
        {
            get
            {
                return typeof(T).Name;
            }
        }

        protected T DataRowToModel(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }

            object result = new object();
            switch (typeof(T).FullName)
            {
                case "MilitaryShip":
                    result = new MilitaryShip()
                    {
                        Id = dr.Field<int>("id"),
                        Length = dr.Field<int>("Length"),
                        Range = dr.Field<int>("Range"),
                        Dx = dr.Field<int>("Dx"),
                        Dy = dr.Field<int>("Dy"),
                        PlayingFieldId = dr.Field<int>("PlayingFieldId"),
                        ShipTypeId = dr.Field<int>("ShipTypeId")
                    };
                    break;
            }
            return (T)result;
        }

        protected ICollection<T> DataTableToCollection(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            return dt.AsEnumerable().Select(x => DataRowToModel(x)).ToList();
        }

    
        public string GetById(int id)
        {
            string result = "";
            var query = $"select * from {TableName} where id = {id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while(reader.Read())
                    {
                        result = "Ship #" + reader["Id"] + " was founded " +
                                  " Range = "+ reader["Range"] + 
                                  " Length = " + reader["Length"] + 
                                  " Dx = " + reader["Dx"] + 
                                  " Dy = " + reader["Dy"] + "\n";
                    }    
                }
                finally
                {
                    // Always call Close when done reading.
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
            var query = $"delete from {TableName} where id = {entity.Id}";
            ExecuteQuery(query);
        }

        public void GetAll()
        {
            var query = $"select * from {TableName}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                List<T> res = new List<T>();
                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                       //add to res
                    }
                }
                reader.Close();
            }  
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
            var query = $"update {TableName} set {columnsValues} where Id = {id}";
            ExecuteQuery(query);
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

            var query = $"insert into {TableName} ({columns}) values ({values})";
            ExecuteQuery(query);
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
            }
        }
    }
}
