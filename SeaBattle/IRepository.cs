namespace СustomORM
{
    using System.Collections.Generic;
    interface IRepository<T> where T : class
    {
        string GetById(int id);
        List<string> GetAll();
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
