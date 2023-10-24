using chdTour.DataAccess.Contracts.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain.Base
{
    public abstract class BaseEntity<PK> : IBaseEntity<PK> where PK : struct
    {
        public PK Id { get; set; }
        public DateTime Created { get; set; }
    }
}
