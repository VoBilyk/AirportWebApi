using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class AeroplaneRepository : GenericRepository<Aeroplane>
    {
        public AeroplaneRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Aeroplane>> GetAllAsync()
        {
            return await dbSet.Include(i => i.AeroplaneType).ToListAsync();
        }

        public override async Task<Aeroplane> GetAsync(Guid id)
        {
            Aeroplane item = await dbSet.Include(i => i.AeroplaneType).SingleOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id: {id}");
            }

            return item;
        }
    }
}
