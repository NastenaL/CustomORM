namespace СustomORM
{
    using System.Collections.Generic;
    interface IRepository<T> where T : class
    {
        void GetById(int id);
        ICollection<T> GetAll();
        ICollection<T> GetAll(string where);
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
        void Delete(ICollection<T> entityes);
    }
}
