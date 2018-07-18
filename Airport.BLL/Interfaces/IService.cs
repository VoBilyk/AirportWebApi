using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Airport.BLL.Interfaces
{
    public interface IService<TDto>
    {
        Task<TDto> GetAsync(Guid id);

        Task<List<TDto>> GetAllAsync();

        Task<TDto> CreateAsync(TDto dto);

        Task<TDto> UpdateAsync(Guid id, TDto dto);

        Task DeleteAsync(Guid id);

        Task DeleteAllAsync();
    }
}
