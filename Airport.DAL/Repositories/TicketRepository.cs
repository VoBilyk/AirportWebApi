using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Entities;


namespace Airport.DAL.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository(AirportContext contex) : base(contex) { }

        public override async Task<List<Ticket>> GetAllAsync()
        {
            return await dbSet.Include(i => i.Flight).ToListAsync();
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
