namespace SeaBattleBasic.Ships
{
    using SeaBattleBasic.Interfaces;

    public class MixShip : Ship, IAbstractMixShip
    {
        public string Repair()
        {
            return "Repair by MixShip" + this.Range;
        }

        public string Shoot()
        {
            return "Shoot by MixShip" + this.Range;
        }
    }
}