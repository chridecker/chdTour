using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class TourAttachement : BaseAttachmentEntity<Guid>
    {
        public Guid TourId { get; set; }
        public string Title { get; set; }
        public virtual Tour Tour { get; set; }


    }
}
