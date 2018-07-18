using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository(AirportContext contex) : base(contex) { }

        public override List<Ticket> GetAll()
        {
            return dbSet.Include(i => i.Flight).ToList();
        }

        public override Ticket Get(Guid id)
        {
            var item = dbSet.Include(i => i.Flight).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }
    }
}
