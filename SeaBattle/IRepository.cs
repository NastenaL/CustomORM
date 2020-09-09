namespace СustomORM
{
    using System.Collections.Generic;
    interface IRepository<T> where T : class
    {
        T GetById(int id);
        List<object> GetAll();
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
