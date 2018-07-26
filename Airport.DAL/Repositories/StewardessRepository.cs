using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class StewardessRepository : GenericRepository<Stewardess>
    {
        public StewardessRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Stewardess>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Crew).ToListAsync();
        }

        public override async Task<Stewardess> GetAsync(Guid id)
        {
            var item = await dbSet.Include(i => i.Crew).SingleOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id: {id}");
            }

            return item;
        }
    }
}
