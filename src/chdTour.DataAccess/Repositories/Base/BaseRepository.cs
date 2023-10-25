using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using chdTour.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this._chdTourContext.AddAsync(entity, cancellationToken);
            await this._chdTourContext.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this._chdTourContext.Remove(entity);
            await this._chdTourContext.SaveChangesAsync(cancellationToken);
        }


        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            this._chdTourContext.Update<TEntity>(entity);
            await this._chdTourContext.SaveChangesAsync(cancellationToken);
        }
    }
}
