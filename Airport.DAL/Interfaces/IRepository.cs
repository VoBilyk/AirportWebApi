using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Airport.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();

        TEntity Get(Guid id);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(Guid id);

        void Delete();
    }
}
