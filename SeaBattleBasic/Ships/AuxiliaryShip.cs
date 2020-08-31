namespace SeaBattleBasic.Ships
{
    using SeaBattleBasic.Interfaces;
    public class AuxiliaryShip : Ship, IAbstractAuxiliaryShip
    {
        public string Repair()
        {
            return "Repair by Auxiliary with range" + this.Range;
        }
    }
}