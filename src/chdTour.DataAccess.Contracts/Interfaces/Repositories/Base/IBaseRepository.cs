using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace chdTour.DataAccess.Contracts.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
    }
}
