using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class DepartureRepository : GenericRepository<Departure>
    {
        public DepartureRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Departure>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Crew).Include(i => i.Airplane).ToListAsync();
        }

        public override async Task<Departure> GetAsync(Guid id)
        {
            Departure item = await dbSet.Include(i => i.Crew).Include(i => i.Airplane).SingleOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id: {id}");
            }

            return item;
        }
    }
}
