namespace СustomORM
{
    using System.Collections.Generic;
    interface IRepository<T> where T : class
    {
        void GetById(int id);
        void GetAll();
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
