namespace SeaBattleBasic.Interfaces
{
    public interface IAbstractMilitaryShip : IShootable
    {
        new string Shoot();
    }
}