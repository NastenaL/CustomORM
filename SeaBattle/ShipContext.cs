namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System.Data.Entity;
    public class ShipContext : DbContext
    {
        public ShipContext() : base("DefaultConnection")
        { }
        public DbSet<Ship> Ships { get; set; }
    }
}