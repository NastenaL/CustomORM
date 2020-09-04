namespace SeaBattle
{
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System;


    class Program
    {
        static void Main(string[] args)
        {
            ShipType military = new ShipType
            {
                Id = 1,
                Description = "Military"
            };
            ShipType auxiliary = new ShipType
            {
                Id = 2,
                Description = "Auxiliary"
            };
            ShipType mix = new ShipType
            {
                Id = 3,
                Description = "Mix"
            };

            AuxiliaryShip auxiliaryShip = new AuxiliaryShip
            {
                Dx = 1,
                Dy = 0,
                Range = 1,
                Length = 2
            };
            MilitaryShip militaryShip = new MilitaryShip
            {
                Dx = 1,
                Dy = 0,
                Range = 1,
                Length = 2
            };
           
            
            Console.ReadKey();
        }
    }
}