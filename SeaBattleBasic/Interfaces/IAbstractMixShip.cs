namespace SeaBattleBasic.Interfaces
{
    public interface IAbstractMixShip : IShootable, IRepairable
    {
        string Repair();
        string Shoot();
    }
}