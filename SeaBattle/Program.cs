namespace SeaBattle
{
    using SeaBattle.ORM;
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

            Repository<ShipType> shipTypeRepository = new Repository<ShipType>();
            //shipTypeRepository.Insert(military);
            //shipTypeRepository.Insert(auxiliary);
            //shipTypeRepository.Insert(mix);
            MixShip mixShip = new MixShip
            {
                Id = 3,
                Dx = 0,
                Dy = 2,
                Range = 4,
                Size = 3
            };

            AuxiliaryShip auxiliaryShip = new AuxiliaryShip
            {
                Id = 5,
                Dx = 2,
                Dy = 0,
                Range = 3,
                Size = 5
            };
     
            MilitaryShip militaryShip = new MilitaryShip
            {
                Id = 6,
                Dx = 1,
                Dy = 0,
                Range = 1,
                Size = 2,
            };

            Repository<MixShip> mixShipRepository = new Repository<MixShip>();
          //  mixShip.Size = 1;
            //mixShipRepository.Update(mixShip);
           // mixShipRepository.Insert(mixShip);
            //var mixShips = mixShipRepository.GetAll();
            Repository<AuxiliaryShip> auxiliaryShipRepository = new Repository<AuxiliaryShip>();
            //auxiliaryShipRepository.Insert(auxiliaryShip);
            //var auxiliaryShips = auxiliaryShipRepository.GetAll();
            //List<Ship> ships = new List<Ship>();
            //ships.AddRange(mixShips);
            //ships.AddRange(auxiliaryShips);

            PlayingField field = new PlayingField
            {
                Id = 1
            };
            
            Repository<PlayingField> fieldRepository = new Repository<PlayingField>();
            //fieldRepository.Insert(field);
      
            var d = fieldRepository.GetById(1);
            Console.ReadKey();
        }
    }
}