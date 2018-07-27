using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class PilotRepository : GenericRepository<Pilot>
    {
        public PilotRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Pilot>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Crews).ToListAsync();
        }

        public override async Task<Pilot> GetAsync(Guid id)
        {
            var item = await dbSet.Include(i => i.Crews).SingleOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id: {id}");
            }

            return item;
        }
    }
}
