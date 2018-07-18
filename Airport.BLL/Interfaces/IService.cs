using System;
using System.Collections.Generic;

namespace Airport.BLL.Interfaces
{
    public interface IService<TDto>
    {
        TDto Get(Guid id);

        List<TDto> GetAll();

        TDto Create(TDto dto);

        TDto Update(Guid id, TDto dto);

        void Delete(Guid id);

        void DeleteAll();
    }
}
