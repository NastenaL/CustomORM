namespace СustomORM
{
    using System;
    using System.Collections.Generic;

    interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetEntityList();
        T GetEntity(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
