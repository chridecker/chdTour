using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class Tour : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public DateTime TourDate { get; set; } = DateTime.Now;
        public int Pitches { get; set; }
        public Guid TourTypeId { get; set; }
        public Guid? GradeId { get; set; }
        public virtual TourType TourType { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<TourPartner> TourPartners { get; set; } = [];
    }
}
