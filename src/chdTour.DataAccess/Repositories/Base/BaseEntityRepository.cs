using chdTour.DataAccess.Contracts.Interfaces.Domain;
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
    public abstract class BaseEntityRepository<TEntity, PK> : BaseRepository<TEntity>, IBaseEntityRepository<TEntity, PK> where TEntity : class, IBaseEntity<PK> where PK : struct
    {

        protected BaseEntityRepository(chdTourContext chdTourContext) : base(chdTourContext)
        {

        }


        public abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        public async Task<TEntity?> GetAsync(PK id, CancellationToken cancellationToken = default)
       => await this._chdTourContext.FindAsync<TEntity>(new[] { id }, cancellationToken);

    }
}
