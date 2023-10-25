using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class TourPartner
    {
        public Guid TourId { get; set; }
        public Guid PartnerId { get; set; }

        public virtual Tour TourObj { get; set; }
        public virtual Person PersonObj { get; set; }
    }
}
