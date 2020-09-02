namespace SeaBattle
{
    using SeaBattleBasic.Ships;
    using System;


    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Repository<Ship> shipRepository = unitOfWork.Repository<Ship>();
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
            shipRepository.Insert(auxiliaryShip);
            shipRepository.Insert(militaryShip);

            auxiliaryShip.Length = 1;
            shipRepository.Update(auxiliaryShip);
            shipRepository.Delete(militaryShip);
            var ship = shipRepository.GetById(3);
            Console.ReadKey();
        }
    }
}