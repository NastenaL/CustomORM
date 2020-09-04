using System.Collections.Generic;
using System.Data;
using System.Linq;
using СustomORM;

namespace SeaBattle
{
    public class AbstractRepository<T> : IRepository<T> where T : class
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
          
            return new typeof(T).FullName
            {
                Id = dr.Field<int>("id"),
                Name = dr.Field<string>("name"),
                Surname = dr.Field<string>("surname"),
                Age = dr.Field<int>("age")

            };
        }

        protected ICollection<T> DataTableToCollection(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            return dt.AsEnumerable().Select(x => DataRowToModel(x)).ToList();
        }

        public T GetById(int id)
        {
            var query = $"select * from {TableName} where id = {id}";
            //the data access layer is implemented elsewhere
            DataRow dr = new DAL.SelectDataRow(query);
            return DataRowToModel(dr);
        }

        public void Delete(T entity)
        {
            if (entity.Id == 0)
            {
                return;
            }
            var query = $"delete from {TableName} where id = {entity.Id}";
            DAL.Query(query);
        }

        public void Delete(ICollection<T> entityes)
        {
            var collectionId = IdentityCollectionToSqlIdFormat(entityes);
            if (string.IsNullOrEmpty(collectionId))
            {
                return;
            }
            var query = $"delete from {TableName} where id in ({collectionId})";
            DAL.Query(query);
        }

        public ICollection<T> GetAll()
        {
            var query = $"select * from {TableName}";
            DataTable dt = DbUtils.SelectDataTable(query);
            return DataTableToCollection(dt);
        }

        public ICollection<T> GetAll(string where)
        {
            var query = $"select * from {TableName} where {where}";
            DataTable dt = DAL.SelectDataTable(query);
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
