namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System;
    using СustomORM;

    class Program
    {
        static void Main(string[] args)
        {
            //using (ShipContext sqlRepository = new ShipContext())
            //{
            //    Ship ship1 = new AuxiliaryShip { Id = 5, Length = 5, Range = 3, Dx = 4, Dy = 4 };
            //    Ship ship2 = new MilitaryShip { Id = 2, Length = 4, Range = 4, Dx = 2, Dy = 6 };
            //    sqlRepository.Add<Ship>(ship1);
            //    sqlRepository.Add<Ship>(ship2);
            //    ship2.Length = 1;
            //    sqlRepository.Update(ship2);
            //    sqlRepository.Delete<Ship>(ship1);
            //}
           
            Console.ReadKey();
        }
    }
}