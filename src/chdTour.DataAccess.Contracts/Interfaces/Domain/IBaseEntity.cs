using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Interfaces.Domain
{
    public interface IBaseEntity<PK> where PK : struct
    {
        PK Id { get; set; }
        DateTime Created {  get; set; }
    }
}
