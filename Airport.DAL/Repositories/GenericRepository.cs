using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Airport.DAL.Interfaces;


namespace Airport.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbSet<TEntity> dbSet;
        protected AirportContext context;


        public GenericRepository(AirportContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();

        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual TEntity Get(Guid id)
        {
            //var item = dbSet.FindAsync(id);

            //if(item == null)
            //{
            //    throw new ArgumentException($"Can`t find item by id:{id}");
            //}

            //return await item;

            var item = dbSet.Find(id);

            if (item == null)
            {
                throw new ArgumentException($"Can`t find item by id:{id}");
            }

            return item;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual void Create(TEntity item)
        {
            var foundedItem = dbSet.Find(item.Id);

            if (foundedItem != null)
            {
                throw new ArgumentException($"Item id: {item.Id}, has already exist");
            }

            dbSet.Add(item);
        }

        public virtual void Update(TEntity item)
        {
            var foundedItem = dbSet.Find(item.Id);

            if (foundedItem == null)
            {
                throw new ArgumentException($"Item id: {item.Id}, don`t exist");
            }
            
            dbSet.Update(item);
        }

        public virtual void Delete(Guid id)
        {
            var item = dbSet.Find(id);

            if (item == null)
            {
                throw new ArgumentException($"Item id: {item.Id}, don`t exist");
            }
            
            dbSet.Remove(item);
        }

        public virtual void Delete()
        {
            foreach (var item in dbSet)
            {
                dbSet.Remove(item);
            }
        }
    }
}
