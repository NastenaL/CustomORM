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
                Ship user1 = new AuxiliaryShip { Id=5, Length = 3, Range = 1, Dx=4, Dy=4};
                Ship user2 = new MilitaryShip { Id =2, Length = 1, Range = 2, Dx=2, Dy=6};

                db.Ships.Add(user1);
                db.Ships.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");



                var users = db.Ships;
                Console.WriteLine("Список объектов:");
                foreach (Ship u in users)
                {
                    Console.WriteLine("Id = {0}; lenght {1} - {2}", u.Id, u.Length, u.Range);
                }
            }
            Console.ReadKey();
        }
    }
}