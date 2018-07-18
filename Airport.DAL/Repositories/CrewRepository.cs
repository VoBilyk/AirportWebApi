using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class CrewRepository : GenericRepository<Crew>
    {
        public CrewRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Crew>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Pilot).Include(i => i.Stewardesses).ToListAsync();
        }

        public override Crew Get(Guid id)
        {
            var item = dbSet.Include(i => i.Pilot).Include(i => i.Stewardesses).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }
    }
}
