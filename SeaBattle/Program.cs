namespace SeaBattle
{
    using SeaBattle.ORM;
    using SeaBattleBasic;
    using SeaBattleBasic.Ships;
    using System;
    using System.Collections.Generic;

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

            Repository<ShipType> shipTypeRepository = new Repository<ShipType>();
            //shipTypeRepository.Insert(military);
            //shipTypeRepository.Insert(auxiliary);
            //shipTypeRepository.Insert(mix);
            MixShip mixShip = new MixShip
            {
                Id = 1,
                Dx = 3,
                Dy = 1,
                Range = 1,
                Length = 2
            };

            AuxiliaryShip auxiliaryShip = new AuxiliaryShip
            {
                Id = 1,
                Dx = 2,
                Dy = 0,
                Range = 3,
                Length = 5
            };
     
            MilitaryShip militaryShip = new MilitaryShip
            {
                Id = 1,
                Dx = 1,
                Dy = 0,
                Range = 1,
                Length = 2,

            };

            Repository<MixShip> mixShipRepository = new Repository<MixShip>();
            //mixShipRepository.Insert(mixShip);
            var mixShips = mixShipRepository.GetAll();
            Repository<AuxiliaryShip> auxiliaryShipRepository = new Repository<AuxiliaryShip>();
            //auxiliaryShipRepository.Insert(auxiliaryShip);
            var auxiliaryShips = auxiliaryShipRepository.GetAll();
            List<Ship> ships = new List<Ship>();
            ships.AddRange(mixShips);
            ships.AddRange(auxiliaryShips);
            Console.ReadKey();
        }
    }
}