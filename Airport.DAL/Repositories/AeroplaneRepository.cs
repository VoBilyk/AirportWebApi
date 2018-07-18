using System;
using System.Linq;
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

        public override Aeroplane Get(Guid id)
        {
            var item = dbSet.Include(i => i.AeroplaneType).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }
    }
}
