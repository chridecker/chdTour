﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chdTour.Persistence.EF;

#nullable disable

namespace chdTour.Persistence.EF.Migrations
{
    [DbContext(typeof(chdTourContext))]
    partial class chdTourContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GradeScalaId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GradeScalaId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.GradeScala", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GradeScalas");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GradeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pitches")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TourDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TourTypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.HasIndex("TourTypeId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.TourPartner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TourId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.HasIndex("TourId");

                    b.ToTable("TourPartners");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.TourType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GradeScalaId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GradeScalaId");

                    b.ToTable("TourTypes");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Grade", b =>
                {
                    b.HasOne("chdTour.DataAccess.Contracts.Domain.GradeScala", "GradeScala")
                        .WithMany("Grades")
                        .HasForeignKey("GradeScalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GradeScala");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Tour", b =>
                {
                    b.HasOne("chdTour.DataAccess.Contracts.Domain.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("chdTour.DataAccess.Contracts.Domain.TourType", "TourType")
                        .WithMany("Tours")
                        .HasForeignKey("TourTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("TourType");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.TourPartner", b =>
                {
                    b.HasOne("chdTour.DataAccess.Contracts.Domain.Person", "PersonObj")
                        .WithMany("TourPartners")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chdTour.DataAccess.Contracts.Domain.Tour", "TourObj")
                        .WithMany("TourPartners")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonObj");

                    b.Navigation("TourObj");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.TourType", b =>
                {
                    b.HasOne("chdTour.DataAccess.Contracts.Domain.GradeScala", "GradeScala")
                        .WithMany()
                        .HasForeignKey("GradeScalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GradeScala");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.GradeScala", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Person", b =>
                {
                    b.Navigation("TourPartners");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.Tour", b =>
                {
                    b.Navigation("TourPartners");
                });

            modelBuilder.Entity("chdTour.DataAccess.Contracts.Domain.TourType", b =>
                {
                    b.Navigation("Tours");
                });
#pragma warning restore 612, 618
        }
    }
}
