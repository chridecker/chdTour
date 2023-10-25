using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Domain.Base;
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
    public class GradeScalaRepository : BaseEntityRepository<GradeScala, Guid>, IGradeScalaRepository
    {
        public GradeScalaRepository(chdTourContext chdTourContext) : base(chdTourContext)
        {
        }

        public override async Task<IEnumerable<GradeScala>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this._chdTourContext.GradeScalas.ToListAsync(cancellationToken);
    }
}
