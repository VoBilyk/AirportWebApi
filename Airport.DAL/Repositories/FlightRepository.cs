using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    public class FlightRepository : GenericRepository<Flight>
    {
        public FlightRepository(AirportContext contex) : base(contex) { }

        public override List<Flight> GetAll()
        {
            return dbSet.Include(i => i.Tickets).ToList();
        }

        public override Flight Get(Guid id)
        {
            var item = dbSet.Include(i => i.Tickets).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }
    }
}
