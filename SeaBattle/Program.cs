namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System;
    using СustomORM;

    class Program
    {
        static void Main(string[] args)
        {
            using (SQLShipRepository sqlRepository = new SQLShipRepository())
            {
                Ship ship1 = new AuxiliaryShip { Id = 5, Length = 5, Range = 3, Dx = 4, Dy = 4 };
                Ship ship2 = new MilitaryShip { Id = 2, Length = 4, Range = 4, Dx = 2, Dy = 6 };
                sqlRepository.Create(ship1);
                sqlRepository.Create(ship2);
                //sqlRepository.Save();
                //Console.WriteLine("Объекты успешно сохранены");

                //var users = sqlRepository.GetEntityList();
                //Console.WriteLine("Список объектов:");
                //foreach (Ship u in users)
                //{
                //    Console.WriteLine("Id = {0}; lenght {1} - {2}", u.Id, u.Length, u.Range);
                //}
                //ship2.Length = 1;
                //sqlRepository.Update(ship2);
                //sqlRepository.Save();
                //var e = sqlRepository.GetEntity(2);
             
            }
           
            Console.ReadKey();
        }
    }
}