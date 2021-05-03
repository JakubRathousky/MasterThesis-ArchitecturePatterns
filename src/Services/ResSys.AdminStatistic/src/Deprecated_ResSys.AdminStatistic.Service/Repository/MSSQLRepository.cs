using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
// using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ResSys.Common;
using ResSys.AdminStatistic.Service.Data;

namespace ResSys.AdminStatistic.Service.Repository
{
    public class MSSQLRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected AdminStatisticContext context;
        protected DbSet<TEntity> dbSet;


        public MSSQLRepository(AdminStatisticContext dataContext)
        {
            this.context = dataContext;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).ToListAsync();
        }
        public async Task<TEntity> GetAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).SingleOrDefaultAsync();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await context.AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateOneAsync(TEntity entity, string filterPropPath, object filterPropVal, string updatePropPath, object updatePropValue)
        {
        }

        public async Task RemoveAsync(Guid id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            context.Remove(entityToDelete);
        }
    }
}