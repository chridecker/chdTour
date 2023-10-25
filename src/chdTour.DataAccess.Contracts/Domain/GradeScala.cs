using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class GradeScala : BaseEntity<Guid>
    {
        public string Title { get; set; }

    }
}
