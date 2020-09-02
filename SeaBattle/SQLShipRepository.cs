namespace СustomORM
{
    using SeaBattle;
    using SeaBattleBasic.Ships;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class SQLShipRepository 
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SeaBattle;Integrated Security=True";
        
        public SQLShipRepository()
        {
            db = new ShipContext();
        }
        private ShipContext db;

        public IEnumerable<Ship> GetEntityList()
        {
            return db.Ships;
        }

        public Ship GetEntity(int id)
        {
            return db.Ships.Find(i => i.Id == id);
        }

        public void Create(Ship ship)
        {
            string sqlExpression = "INSERT INTO Ship (Length, Range, Dx, Dy ) "+
                                   "VALUES ("+ship.Length+", "+ship.Range+", "+ship.Dx+", "+ship.Dy+")";
            EcecuteQuery(sqlExpression);

            db.Ships.Add(ship);
        }
        private void EcecuteQuery(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }
        }
        public void Update(Ship ship)
        {
            string sqlExpression = "UPDATE  Ship  SET Length =" + ship.Length + ", Range =" 
                                    + ship.Range + ", Dx = " + ship.Dx + ", Dy =" + ship.Dy 
                                    + " WHERE Id ="+ ship.Id;
            EcecuteQuery(sqlExpression);
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Ship WHERE Id =" + id;
            EcecuteQuery(sqlExpression);
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
