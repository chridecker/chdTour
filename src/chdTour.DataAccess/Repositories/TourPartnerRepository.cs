﻿using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using chdTour.DataAccess.Repositories.Base;
using chdTour.Persistence.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.DataAccess.Repositories
{
    public class TourPartnerRepository : BaseRepository<TourPartner>, ITourPartnerRepository
    {
        public TourPartnerRepository(chdTourContext chdTourContext) : base(chdTourContext)
        {
        }
    }
}