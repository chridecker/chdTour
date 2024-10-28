using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class TourImage : BaseAttachmentEntity<Guid>
    {
        public string Title { get; set; }
        public Guid TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
