namespace SeaBattle
{
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using СustomORM;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SeaBattle;Integrated Security=True";

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
            var type = typeof(T).FullName;
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

    
        public void GetById(int id)
        {
            
            var query = $"select * from {TableName} where id = {id}";
            ExecuteQuery(query);
          //  return DataRowToModel(dr);
        }

        public void Delete(T entity)
        {
            if (entity.Id == 0)
            {
                return;
            }
            var query = $"delete from {TableName} where id = {entity.Id}";
            ExecuteQuery(query);
          //  DAL.Query(query);
        }

        public ICollection<T> GetAll()
        {
            var query = $"select * from {TableName}";
            DataTable dt = new DataTable();// DAL.SelectDataTable(query);
            return DataTableToCollection(dt);
        }

        public ICollection<T> GetAll(string where)
        {
            var query = $"select * from {TableName} where {where}";
            DataTable dt = new DataTable(); //DAL.SelectDataTable(query);
            return DataTableToCollection(dt);
        }

        protected string IdentityCollectionToSqlIdFormat(ICollection<T> collection)
        {
            var array = collection.Select(x => x.Id);
            return string.Join(",", array);
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
