namespace SeaBattle
{
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

            AuxiliaryShip auxiliaryShip = new AuxiliaryShip
            {
                Id = 2,
                Dx = 1,
                Dy = 0,
                Range = 1,
                Length = 2,
                ShipTypeId = 2
            };
            MilitaryShip militaryShip = new MilitaryShip
            {
                Id = 1,
                Dx = 1,
                Dy = 0,
                Range = 1,
                Length = 2,
                ShipTypeId = 1
            };

            Repository<Ship> shipRepository = new Repository<Ship>();
            // shipRepository.Insert(militaryShip);
            //shipRepository.Insert(auxiliaryShip);
            //militaryShip.Length = 3;
            //shipRepository.Update(militaryShip);

            //shipRepository.Delete(auxiliaryShip);
            List<object> re = shipRepository.GetById(2);
            //List<object> res = shipRepository.GetAll();

            Console.ReadKey();
        }
    }
}