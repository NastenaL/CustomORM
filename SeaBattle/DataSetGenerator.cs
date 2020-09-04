namespace SeaBattle
{
    using System.Data;
    public static class DataSetGenerator
    {
        public static DataSet ShipDataSet()
        {
            DataTable shipsDataTable = new DataTable();
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Id",
                DataType = typeof(int)
            });
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Length",
                DataType = typeof(int)
            });
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Range",
                DataType = typeof(int)
            });
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Dx",
                DataType = typeof(int)
            });
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Dy",
                DataType = typeof(int)
            });
            shipsDataTable.Columns.Add(new DataColumn()
            {
                ColumnName = "PlayingFieldId",
                DataType = typeof(int)
            });
            //Add Ships

            DataSet shipsDataSet = new DataSet();
            shipsDataSet.Tables.Add(shipsDataTable);

            return shipsDataSet;
        }

        public static DataSet PlayingFieldGenerator()
        {
            DataTable playingFieldsTable = new DataTable();
            playingFieldsTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Width",
                DataType = typeof(int)
            });
            playingFieldsTable.Columns.Add(new DataColumn()
            {
                ColumnName = "Heigth",
                DataType = typeof(int)
            });
           

            //Add playing field

            DataSet playingFieldDataSet = new DataSet();
            playingFieldDataSet.Tables.Add(playingFieldsTable);

            return playingFieldDataSet;
        }
    }
}
