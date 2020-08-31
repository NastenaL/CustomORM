namespace SeaBattleBasic.Ships
{
    using SeaBattleBasic.Interfaces;

    public class MilitaryShip : Ship, IAbstractMilitaryShip
    {
        public string Shoot()
        {
            return "Shoot by Military ship" + this.Range;
        }
    }
}