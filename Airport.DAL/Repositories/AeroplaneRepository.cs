using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class AeroplaneRepository : GenericRepository<Aeroplane>
    {
        public AeroplaneRepository(AirportContext contex) : base(contex) { }

        public override List<Aeroplane> GetAll()
        {
            return dbSet.Include(i => i.AeroplaneType).ToList();
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
