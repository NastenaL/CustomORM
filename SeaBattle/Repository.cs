namespace SeaBattle
{
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
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
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
            var query = $"select * from {TableName} where id = {id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int number = command.ExecuteNonQuery();
            }
          //  return DataRowToModel(dr);
        }

        public void Delete(T entity)
        {
            if (entity.Id == 0)
            {
                return;
            }
            var query = $"delete from {TableName} where id = {entity.Id}";
          //  DAL.Query(query);
        }

        public void Delete(ICollection<T> entityes)
        {
            var collectionId = IdentityCollectionToSqlIdFormat(entityes);
            if (string.IsNullOrEmpty(collectionId))
            {
                return;
            }
            var query = $"delete from {TableName} where id in ({collectionId})";
         //   DAL.Query(query);
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

        }
        public void Insert(T entity)
        {

        }
    }
}
