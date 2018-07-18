using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.BLL.Interfaces
{
    public interface IService<TDto>
    {
        TDto Get(Guid id);

        Task<List<TDto>> GetAllAsync();

        TDto Create(TDto dto);

        TDto Update(Guid id, TDto dto);

        void Delete(Guid id);

        void DeleteAll();
    }
}
