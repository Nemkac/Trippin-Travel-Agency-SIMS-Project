﻿// <auto-generated />
using System;
using InitialProject.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InitialProject.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20230315234559_addedKeyPoints")]
    partial class addedKeyPoints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingCancelPeriodDays")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestLimit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("locationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("minDaysBooked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("type")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("locationId");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LocationsOfAccommodations");
                });

            modelBuilder.Entity("InitialProject.Model.KeyPoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Tourid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("visited")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("Tourid");

                    b.ToTable("KeyPoints");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("hoursDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("language")
                        .HasColumnType("INTEGER");

                    b.Property<int>("locationid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("startDates")
                        .HasColumnType("TEXT");

                    b.Property<int>("touristLimit")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("locationid");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.TourLocation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("TourLocation");
                });

            modelBuilder.Entity("InitialProject.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.HasOne("InitialProject.Model.AccommodationLocation", "location")
                        .WithMany()
                        .HasForeignKey("locationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("InitialProject.Model.KeyPoint", b =>
                {
                    b.HasOne("InitialProject.Model.Tour", null)
                        .WithMany("keyPoints")
                        .HasForeignKey("Tourid");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.HasOne("InitialProject.Model.TourLocation", "location")
                        .WithMany()
                        .HasForeignKey("locationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.Navigation("keyPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
