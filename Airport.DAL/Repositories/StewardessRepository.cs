using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    public class StewardessRepository : GenericRepository<Stewardess>
    {
        public StewardessRepository(AirportContext contex) : base(contex) { }
    }
}
