using chdTour.DataAccess.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace chdTour.Persistence.EF
{
    public class chdTourContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public chdTourContext() : base()
        {
                
        }

        public chdTourContext(DbContextOptions<chdTourContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
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
            //modelBuilder.Entity<Person>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
