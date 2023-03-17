﻿// <auto-generated />
using System;
using InitialProject.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InitialProject.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("locationid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("minDaysBooked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("type")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("locationid");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationLocation", b =>
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

                    b.ToTable("LocationsOfAccommodations");
                });

            modelBuilder.Entity("InitialProject.Model.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("arrival")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("departure")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("stayingPeriod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("InitialProject.Model.GuestRate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("cleanness")
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("overallRating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("respectingRules")
                        .HasColumnType("INTEGER");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("GuestRate");
                });

            modelBuilder.Entity("InitialProject.Model.Image", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Tourid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("imageLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("Tourid");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("InitialProject.Model.KeyPoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("visited")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("tourId");

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
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.HasOne("InitialProject.Model.AccommodationLocation", "location")
                        .WithMany()
                        .HasForeignKey("locationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("InitialProject.Model.Image", b =>
                {
                    b.HasOne("InitialProject.Model.Tour", null)
                        .WithMany("imageLinks")
                        .HasForeignKey("Tourid");
                });

            modelBuilder.Entity("InitialProject.Model.KeyPoint", b =>
                {
                    b.HasOne("InitialProject.Model.Tour", null)
                        .WithMany("keyPoints")
                        .HasForeignKey("tourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.Navigation("imageLinks");

                    b.Navigation("keyPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
