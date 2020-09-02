namespace SeaBattle
{
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;

    public class EFDbContext : DbContext
    {
        public EFDbContext()
              : base("name=DBConnection")
        {
            Database.SetInitializer<EFDbContext>(new CreateDatabaseIfNotExists<EFDbContext>());
            Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().ToTable("Ship");
        }
    }
}