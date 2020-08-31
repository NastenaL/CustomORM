namespace SeaBattleBasic.Interfaces
{
    public interface IAbstractShip
    {
        IAbstractAuxiliaryShip CreateAuxiliaryShip();
        IAbstractMilitaryShip CreateMilitaryShip();
        IAbstractMixShip CreateMixShip();
    }
}
