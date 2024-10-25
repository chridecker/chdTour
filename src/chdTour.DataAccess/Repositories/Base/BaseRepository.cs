using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using chdTour.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly chdTourContext _chdTourContext;

        public BaseRepository(chdTourContext chdTourContext)
        {
            this._chdTourContext = chdTourContext;
            if (!this._chdTourContext.Database.EnsureCreated() && this._chdTourContext.Database.GetPendingMigrations().Any())
            {
                this._chdTourContext.Database.EnsureDeleted();
                this._chdTourContext.Database.Migrate();
            }
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression) => this._chdTourContext.Set<TEntity>().Where(expression);
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression) => await this._chdTourContext.Set<TEntity>().FirstOrDefaultAsync(expression);


        public async Task<bool> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (this._chdTourContext.Entry<TEntity>(entity).State == EntityState.Detached)
            {
                await this._chdTourContext.AddAsync<TEntity>(entity, cancellationToken);
            }
            else
            {
                this._chdTourContext.Update<TEntity>(entity);
            }

            return (await this._chdTourContext.SaveChangesAsync(cancellationToken)) == 1;
        }
        public async Task<IEnumerable<TEntity>> FindAll(CancellationToken cancellationToken) => await this._chdTourContext.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this._chdTourContext.Set<TEntity>().Remove(entity);
            await this._chdTourContext.SaveChangesAsync(cancellationToken);
        }
    }
}
