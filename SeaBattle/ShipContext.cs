namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using СustomORM;

    public class ShipContext : DbContext, IRepository
    {
        public ShipContext() : base("DBConnection")
        {

            Database.SetInitializer<ShipContext>(new DropCreateDatabaseAlways<ShipContext>());
            Ships = new List<Ship>();
        }
        public List<Ship> Ships { get; set; }

        public void Add<T>(T entity) where T : class
        {
            this.Set<T>().Add(entity);

            this.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            this.Set<T>().Attach(entity);
            this.Entry(entity).State = EntityState.Modified;

            this.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            this.Set<T>().Attach(entity);
            this.Set<T>().Remove(entity);

            this.SaveChanges();
        }

        public IQueryable<T> GetIQueryable<T>() where T : class
        {
            return this.Set<T>();
        }
    }

    public class Repository<TEntity>
    where TEntity : class, new()
    {
        public IRepository Context { get; set; }

        public Repository(IRepository context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Delete(entity);
        }

        public List<TEntity> ToList()
        {
            return Context.GetIQueryable<TEntity>().ToList();
        }
    }
}