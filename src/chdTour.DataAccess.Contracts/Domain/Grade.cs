﻿using chdTour.DataAccess.Contracts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain
{
    public class Grade : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public Guid ScalaId { get; set; }
    }
}
