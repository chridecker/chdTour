using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Domain.Base;
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

        public chdTourContext()
        {

        }

        public chdTourContext(DbContextOptions<chdTourContext> options) : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.HandleChangeTrack();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.HandleChangeTrack();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void HandleChangeTrack()
        {
            var time = DateTime.Now;
            foreach (var change in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                var createdDateTime = change.CurrentValues.EntityType.FindProperty(nameof(BaseEntity<Guid>.Created));

                createdDateTime?.FieldInfo?.SetValue(change.Entity, time);
            }
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
            modelBuilder.Entity<Tour>(build =>
            {
                build.HasMany(x => x.TourPartners).WithOne(x => x.TourObj).HasForeignKey(x => x.TourId);
                build.HasOne(x => x.TourType).WithMany(x => x.Tours).HasForeignKey(x => x.TourTypeId);
            });

            modelBuilder.Entity<TourType>().HasOne(x => x.GradeScala).WithMany().HasForeignKey(x => x.GradeScalaId);
            modelBuilder.Entity<GradeScala>().HasMany(x => x.Grades).WithOne(x => x.GradeScala).HasForeignKey(x => x.GradeScalaId);

            modelBuilder.Entity<TourPartner>().HasKey(x => new { x.TourId, x.PartnerId });

            modelBuilder.Entity<Person>().HasMany(x => x.TourPartners).WithOne(x => x.PersonObj).HasForeignKey(x => x.PartnerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
