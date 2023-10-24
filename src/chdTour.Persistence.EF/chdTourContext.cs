using Microsoft.EntityFrameworkCore;
using System;

namespace chdTour.Persistence.EF
{
    public class chdTourContext : DbContext
    {
        public chdTourContext(DbContextOptions<chdTourContext> options) : base(options)
        {
        }
    }
}
