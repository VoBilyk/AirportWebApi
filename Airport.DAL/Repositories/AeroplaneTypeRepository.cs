using Airport.DAL.Entities;

namespace Airport.DAL.Repositories
{
    class AeroplaneTypeRepository : GenericRepository<AeroplaneType>
    {
        public AeroplaneTypeRepository(AirportContext context) : base(context) { }
    }
}
