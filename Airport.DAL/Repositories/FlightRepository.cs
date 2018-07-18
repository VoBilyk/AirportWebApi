using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class FlightRepository : GenericRepository<Flight>
    {
        public FlightRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Flight>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Tickets).ToListAsync();
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
