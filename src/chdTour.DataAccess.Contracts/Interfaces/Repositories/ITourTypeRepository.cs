using chd.UI.Base.Contracts.Interfaces.Data;
using chdTour.DataAccess.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Interfaces.Repositories
{
    public interface ITourTypeRepository : IBaseRepository<TourType,Guid>
    {
    }
}
