using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class Person : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
