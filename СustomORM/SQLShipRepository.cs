namespace СustomORM
{
    using Microsoft.EntityFrameworkCore;
    using SeaBattleBasic.Ships;
    using System;
    using System.Collections.Generic;

    class SQLShipRepository : IRepository<Ship>
    {
        private ShipContext db;

        public IEnumerable<Ship> GetEntityList()
        {
            return db.Ships;
        }

        public Ship GetEntity(int id)
        {
            return db.Ships.Find(id);
        }

        public void Create(Ship ship)
        {
            db.Ships.Add(ship);
        }

        public void Update(Ship book)
        {
          //  db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Ship ship = db.Ships.Find(id);
            if (ship != null)
                db.Ships.Remove(ship);
        }

        public void Save()
        {
          //  db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                  //  db.Dispose();
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
