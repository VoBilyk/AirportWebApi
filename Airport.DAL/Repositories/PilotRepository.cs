using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    public class PilotRepository : GenericRepository<Pilot>
    {
        public PilotRepository(AirportContext contex) : base(contex) { }
    }
}
