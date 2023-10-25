using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Interfaces.Repositories
{
    public interface ITourRepository : IBaseEntityRepository<Tour,Guid>
    {
    }
}
