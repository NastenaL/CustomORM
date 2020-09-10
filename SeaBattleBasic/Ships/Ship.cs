namespace SeaBattleBasic.Ships
{
    using SeaBattle.ORM;
    using SeaBattleBasic.Interfaces;
    using System.Text;

    public abstract class Ship : BaseEntity, IAbstractShip
    {
        public Ship()
        {
            this.Move();
        }

        public int Length { get; set; }
        public int Range { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int PlayingFieldId { get; set; }


        public StringBuilder Move()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("This ship is move in directions dx = {0}, dy ={1}",
                                       this.Dx,
                                       this.Dy);
            return stringBuilder;
        }

        public static string operator >=(Ship a, Ship b)
        {
            return "this operation is not support";
        }

        public static string operator <=(Ship a, Ship b)
        {
            return "this operation is not support";
        }

        public static string operator >(Ship a, Ship b)
        {
            return "this operation is not support";
        }

        public static string operator <(Ship a, Ship b)
        {
            return "this operation is not support";
        }

        private static bool IsEqualsShips(Ship a, Ship b)
        {
            bool res;
            res = a.GetType() == b.GetType() &&
                  b.Length == 1 && a.Length == 1;
            return res;
        }

        //public static bool operator ==(Ship a, Ship b)
        //{
        //    return IsEqualsShips(a, b);
        //}

        public override bool Equals(object obj)
        {
            return obj is Ship ship &&
                   IsEqualsShips(this, ship);
        }

        public IAbstractAuxiliaryShip CreateAuxiliaryShip()
        {
            return new AuxiliaryShip();
        }

        public IAbstractMilitaryShip CreateMilitaryShip()
        {
            return new MilitaryShip();
        }

        public IAbstractMixShip CreateMixShip()
        {
            return new MixShip();
        }


        //public static bool operator !=(Ship a, Ship b)
        //{
        //    return !IsEqualsShips(a, b);
        //}
    }
}