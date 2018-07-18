using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class CrewRepository : GenericRepository<Crew>
    {
        public CrewRepository(AirportContext contex) : base(contex) { }

        public override List<Crew> GetAll()
        {
            return dbSet.Include(i => i.Pilot).Include(i => i.Stewardesses).ToList();
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
