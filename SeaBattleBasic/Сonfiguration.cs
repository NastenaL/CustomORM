namespace SeaBattleBasic
{
    public static class Сonfiguration
    {
        public static int MinMovementVector = -10;
        public static int MaxMovementVector = 10;
        public static int MinLength = 1;
        public static int MaxLength = 5;
        public static int MinCoordinate = -10;
        public static int MaxCoordinate = 10;
        public static int MinRange = 1;
        public static int MaxRange = 5;

        //DB
        public static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SeaBattle;Integrated Security=True";

    }
}