﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TId, TEntity>
      where TEntity : class, new()
    {
        Task<TEntity> GetAsync(TId id, CancellationToken ct = default);

        Task<IEnumerable<TEntity>> GetListAsync(CancellationToken ct = default);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TId id);
    }
}
