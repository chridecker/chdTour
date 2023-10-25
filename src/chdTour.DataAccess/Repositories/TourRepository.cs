using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using chdTour.DataAccess.Repositories.Base;
using chdTour.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.DataAccess.Repositories
{
    public class TourRepository : BaseEntityRepository<Tour, Guid>, ITourRepository
    {
        public TourRepository(chdTourContext chdTourContext) : base(chdTourContext)
        {
        }

        public override async Task<IEnumerable<Tour>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this._chdTourContext.Tours.ToListAsync(cancellationToken);
    }
}
