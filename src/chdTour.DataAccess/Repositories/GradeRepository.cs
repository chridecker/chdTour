﻿using chdTour.DataAccess.Contracts.Domain;
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
    public class GradeRepository : BaseEntityRepository<Grade, Guid>, IGradeRepository
    {
        public GradeRepository(chdTourContext chdTourContext) : base(chdTourContext)
        {
        }

        public override async Task<IEnumerable<Grade>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this._chdTourContext.Grades.ToListAsync(cancellationToken);
    }
}
