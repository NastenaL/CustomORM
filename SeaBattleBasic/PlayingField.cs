namespace SeaBattleBasic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;
    using System.Linq;
    using SeaBattleBasic.Ships;

    [Table("PlayingField")]
    public class PlayingField
    {
        public PlayingField()
        {
            this.Ships = new Dictionary<Point, Ship>();
        }

        private Ship[] indexArr = new Ship[5];
        public Ship[] AddToIndexArr(int size, Ship value)
        {
            if (indexArr == null)
            {
                indexArr = new Ship[size - 1];
            }
            indexArr.SetValue(value, size - 1);
            return indexArr;
        }

        public Ship this[int q]
        {
            get { return indexArr[q]; }
            set { indexArr[q] = value; }
        }

        public Dictionary<Point, Ship> Ships { get; set; }

        public Ship AddShip(Point startPoint, ShipType type)
        {
            Random random = new Random();
            Ship ship = new MixShip
            {
                Range = random.Next(Сonfiguration.MinRange, Сonfiguration.MaxRange)
            };
            switch (type.Description)
            {
                case "Auxiliary":
                    ship.CreateAuxiliaryShip();
                    break;
                case "Military":
                    ship.CreateMilitaryShip();
                    break;
                case "Mix":
                    ship.CreateMixShip();
                    break;
            }

            this.InitializeShip(ref ship, startPoint);
            this.Ships.Add(startPoint, ship);
            AddToIndexArr(Ships.Count, ship);
            var i = this.GenerateIndex(this.GetQuadrant(startPoint), startPoint);
            return ship;
        }

        public Point InitNewPoint()
        {
            Random random = new Random();
            Point point = new Point
            {
                X = random.Next(Сonfiguration.MinCoordinate, Сonfiguration.MaxCoordinate),
                Y = random.Next(Сonfiguration.MinCoordinate, Сonfiguration.MaxCoordinate)
            };
            return point;
        }

        public Dictionary<Point, Ship> GetAllShips()
        {
            return this.SortByCenterDistance();
        }

        private Dictionary<Point, Ship> SortByCenterDistance()
        {
            return this.Ships.OrderBy(obj => GetCenterDistance(obj.Key)).ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private int GenerateIndex(byte quadrant, Point shipPoint)
        {
            var coords = ConcatInt(Math.Abs(shipPoint.X), Math.Abs(shipPoint.Y));
            return ConcatInt(quadrant, coords);
        }

        private static int ConcatInt(int a, int b)
        {
            int bLength = (int)Math.Ceiling(Math.Log10(b));
            int ab = (a * ((int)Math.Pow(10, bLength))) + b;
            return (int)ab;
        }
        private byte GetQuadrant(Point shipPoint)
        {
            if (shipPoint.X >= 0)
                return shipPoint.Y >= 0 ? (byte)1 : (byte)4;
            return shipPoint.Y >= 0 ? (byte)2 : (byte)3;
        }

        private void InitializeShip(ref Ship ship, Point coordinates)
        {
            Random random = new Random();
            ship.Dx = random.Next(Сonfiguration.MinMovementVector, Сonfiguration.MaxMovementVector);
            ship.Dy = random.Next(Сonfiguration.MinMovementVector, Сonfiguration.MaxMovementVector);
            ship.Size = random.Next(Сonfiguration.MinLength, Сonfiguration.MaxLength);
        }

        private double GetCenterDistance(Point coordinates)
        {
            return Math.Sqrt(Math.Pow(coordinates.X - 0, 2) + Math.Pow(coordinates.Y - 0, 2));
        }
    }
}