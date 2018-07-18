using Airport.DAL.Entities;

namespace Airport.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Ticket> TicketRepository { get; }

        IRepository<Aeroplane> AeroplaneRepository { get; }

        IRepository<AeroplaneType> AeroplaneTypeRepository { get; }

        IRepository<Crew> CrewRepositiry { get; }

        IRepository<Departure> DepartureRepository { get; }

        IRepository<Flight> FlightRepository { get; }

        IRepository<Pilot> PilotRepositiry { get; }

        IRepository<Stewardess> StewardessRepositiry { get; }

        void SaveChanges();
    }
}
