namespace СustomORM
{
    using System.Collections.Generic;
    using System.Linq;
    interface IRepository<T> where T : class
    {
        T GetById(int id);
        ICollection<T> GetAll();
        ICollection<T> GetAll(string where);
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
        void Delete(ICollection<T> entityes);
    }

    //public interface IRepository
    //{
    //    void Add<T>(T entity)
    //     where T : class;

    //    void Update<T>(T entity)
    //        where T : class;

    //    void Delete<T>(T entity)
    //        where T : class;

    //    IQueryable<T> GetIQueryable<T>()
    //        where T : class;
    //}
}
