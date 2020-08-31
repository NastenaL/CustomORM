namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            using (ShipContext db = new ShipContext())
            {
                Ship user1 = new AuxiliaryShip { Id=125, Length = 3, Range = 1};
                Ship user2 = new MilitaryShip { Id =562, Length = 1, Range = 2};

                db.Ships.Add(user1);
                db.Ships.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

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