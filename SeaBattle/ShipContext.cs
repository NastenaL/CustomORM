namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ShipContext : DbContext
    {
        public ShipContext() : base("DBConnection")
        {

            Database.SetInitializer<ShipContext>(new DropCreateDatabaseAlways<ShipContext>());
            Ships = new List<Ship>();
        }
        public List<Ship> Ships { get; set; }
    }
}