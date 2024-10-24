using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class Person : BaseEntity<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<TourPartner> TourPartners { get; set; } = [];
    }
}
