namespace СustomORM
{
    using System.Linq;

    public interface IRepository
    {
        void Add<T>(T entity)
         where T : class;

        void Update<T>(T entity)
            where T : class;

        void Delete<T>(T entity)
            where T : class;

        IQueryable<T> GetIQueryable<T>()
            where T : class;
    }
}
