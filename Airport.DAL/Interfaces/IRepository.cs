using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Airport.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Guid id);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task CreateAsync(TEntity item);

        Task UpdateAsync(TEntity item);

        Task DeleteAsync(Guid id);

        Task DeleteAsync();
    }
}
