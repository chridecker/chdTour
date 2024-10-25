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
        Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);
        Task<bool> SaveAsync(TEntity entity, CancellationToken cancellationToken);
        System.Linq.IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);
    }
}
