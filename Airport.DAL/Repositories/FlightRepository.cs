using System;
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

        public override async Task<Flight> GetAsync(Guid id)
        {
            Flight item = await dbSet.Include(i => i.Tickets).SingleOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id: {id}");
            }

            return item;
        }
    }
}
