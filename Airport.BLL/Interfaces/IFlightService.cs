using System.Collections.Generic;
using System.Threading.Tasks;

using Airport.Shared.DTO;


namespace Airport.BLL.Interfaces
{
    public interface IFlightService : IService<FlightDto>
    {
        Task<List<FlightDto>> GetWithDelayAsync(int delay);
    }
}
