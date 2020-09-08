namespace СustomORM
{
    interface IRepository<T> where T : class
    {
        string GetById(int id);
        void GetAll();
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
