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
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<TourAttachement> TourAttachements { get; set; }

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
                build.HasMany(x => x.Images).WithOne(x => x.Tour).HasForeignKey(x => x.TourId);
                build.HasMany(x => x.Attachements).WithOne(x => x.Tour).HasForeignKey(x => x.TourId);
                build.HasMany(x => x.TourPartners).WithOne(x => x.TourObj).HasForeignKey(x => x.TourId);
                build.HasOne(x => x.TourType).WithMany(x => x.Tours).HasForeignKey(x => x.TourTypeId);
                build.HasOne(x => x.Grade).WithMany().HasForeignKey(x => x.GradeId);

                build.Navigation(x => x.Grade).AutoInclude();
                build.Navigation(x => x.TourType).AutoInclude();
                build.Navigation(x => x.TourPartners).AutoInclude();
                build.Navigation(x => x.Attachements).AutoInclude();
            });

            modelBuilder.Entity<TourAttachement>(builder =>
            {
                builder.HasOne(x => x.Tour).WithMany(t => t.Attachements).HasForeignKey(x => x.TourId);
            });
            modelBuilder.Entity<TourImage>(builder =>
            {
                builder.HasOne(x => x.Tour).WithMany(t => t.Images).HasForeignKey(x => x.TourId);
            });

            modelBuilder.Entity<TourType>(builder =>
            {
                builder.HasOne(x => x.GradeScala).WithMany().HasForeignKey(x => x.GradeScalaId);
                builder.Navigation(x => x.GradeScala).AutoInclude();
            });
            modelBuilder.Entity<GradeScala>(builder =>
            {
                builder.HasMany(x => x.Grades).WithOne(x => x.GradeScala).HasForeignKey(x => x.GradeScalaId);
                builder.Navigation(x => x.Grades).AutoInclude();
            });

            modelBuilder.Entity<TourPartner>(builder =>
            {
                builder.HasOne(x => x.TourObj).WithMany(x => x.TourPartners).HasForeignKey(x => x.TourId);
                builder.HasOne(x => x.PersonObj).WithMany(x => x.TourPartners).HasForeignKey(x => x.PartnerId);

                builder.Navigation(x => x.TourObj).AutoInclude();
                builder.Navigation(x => x.PersonObj).AutoInclude();
            });

            modelBuilder.Entity<Person>(builder =>
            {
                builder.HasMany(x => x.TourPartners).WithOne(x => x.PersonObj).HasForeignKey(x => x.PartnerId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
