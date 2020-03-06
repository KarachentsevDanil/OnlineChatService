using Microsoft.EntityFrameworkCore;
using OCS.DAL.Entities.Abstract;
using OCS.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.Repositories
{
    public class GenericRepository<TId, TEntity, TContext> : IGenericRepository<TId, TEntity>
        where TEntity : class, IBaseEntity<TId>, new()
        where TContext : DbContext
    {
        public GenericRepository(TContext context)
        {
            DbContext = context;
        }

        protected TContext DbContext { get; }

        public virtual void Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TId id)
        {
            TEntity entity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> GetAsync(TId id, CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        }
    }
}
