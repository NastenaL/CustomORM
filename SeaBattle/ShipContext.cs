namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System.Data.Entity;
    public class ShipContext : DbContext
    {
        public ShipContext() : base("DBConnection")
        {
            Database.SetInitializer<ShipContext>(new DropCreateDatabaseAlways<ShipContext>());
        }
        public DbSet<Ship> Ships { get; set; }
    }
}