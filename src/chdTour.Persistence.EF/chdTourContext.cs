using chdTour.DataAccess.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace chdTour.Persistence.EF
{
    public class chdTourContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<GradeScala> GradeScalas { get; set; }
        public DbSet<TourPartner> TourPartners { get; set; }

        public chdTourContext() : base()
        {

        }

        public chdTourContext(DbContextOptions<chdTourContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=chdTour.db");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourPartner>().HasKey(x => new { x.TourId, x.PartnerId });
            modelBuilder.Entity<Tour>().HasMany(x => x.TourPartners).WithOne(x => x.TourObj);
            modelBuilder.Entity<Person>().HasMany(x => x.TourPartners).WithOne(x => x.PersonObj);

            base.OnModelCreating(modelBuilder);
        }
    }
}
