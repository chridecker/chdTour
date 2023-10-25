using chdTour.DataAccess.Contracts.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chdTour.DataAccess.Contracts.Interfaces.Repositories.Base
{
    public interface IBaseEntityRepository<TEntity, PK> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity<PK> where PK : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(PK id, CancellationToken cancellationToken = default);

    }
}
