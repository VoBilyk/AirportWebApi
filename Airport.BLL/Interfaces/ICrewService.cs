using System.Collections.Generic;
using System.Threading.Tasks;

using Airport.Shared.DTO;


namespace Airport.BLL.Interfaces
{
    public interface ICrewService : IService<CrewDto>
    {
        Task<List<CrewDto>> CreateFromMockApiAsync();
    }
}
