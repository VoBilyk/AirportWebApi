using System;
using System.Collections.Generic;

namespace Airport.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        List<TEntity> GetAll();

        TEntity Get(Guid id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(Guid id);

        void Delete();
    }
}
