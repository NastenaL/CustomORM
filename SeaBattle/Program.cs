namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System;
    using System.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            using (ShipContext db = new ShipContext())
            {
                Ship user1 = new AuxiliaryShip { Id=125, Length = 3, Range = 1, Dx=4, Dy=4};
                Ship user2 = new MilitaryShip { Id =562, Length = 1, Range = 2, Dx=2, Dy=6};

                db.Ships.Add(user1);
                db.Ships.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                Console.WriteLine(connectionString);

                var users = db.Ships;
                Console.WriteLine("Список объектов:");
                foreach (Ship u in users)
                {
                    Console.WriteLine("{0}.{1} - {2}", u.Length, u.Range, u.Speed);
                }
            }
            Console.ReadKey();
        }
    }
}