namespace СustomORM
{
    using SeaBattle;
    using SeaBattleBasic.Ships;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlClient;

    class SQLShipRepository : IRepository<Ship>
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }
            db.Ships.Add(ship);
        }

        public void Update(Ship book)
        {
           db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
      //      Ship ship = db.Ships.Find(id);
      //      if (ship != null)
        //        db.Ships.Remove(ship);
        }

        public void Save()
        {
            db.SaveChanges();
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
