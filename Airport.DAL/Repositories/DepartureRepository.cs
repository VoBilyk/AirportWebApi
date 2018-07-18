using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    public class DepartureRepository : GenericRepository<Departure>
    {
        public DepartureRepository(AirportContext contex) : base(contex) { }

        public override List<Departure> GetAll()
        {
            return dbSet.Include(i => i.Crew).Include(i => i.Airplane).ToList();
        }

        public override Departure Get(Guid id)
        {
            var item = dbSet.Include(i => i.Crew).Include(i => i.Airplane).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }
    }
}
