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
    [Migration("20230425185307_AccommodationRenovationsTable")]
    partial class AccommodationRenovationsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("InitialProject.DTO.RequestDTO", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("guestName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("newArrival")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("newDeparture")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("oldArrival")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("oldDeparture")
                        .HasColumnType("TEXT");

                    b.Property<string>("possible")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("SelectedRequestTransfers");
                });

            modelBuilder.Entity("InitialProject.DTO.TourStatisticsDTO", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("numberOfGuests")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("tourName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("TourStatisticsTransfer");
                });

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

                    b.Property<int>("ownerId")
                        .HasColumnType("INTEGER");

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

                    b.ToTable("AccommodationLocation");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationRate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("cleanness")
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ownerRate")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("AccommodationRates");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationRenovation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationName")
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("duration")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("AccommodationRenovations");
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

                    b.Property<int>("daysToStay")
                        .HasColumnType("INTEGER");

                    b.Property<string>("departure")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("accommodationId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("InitialProject.Model.BookingCancelationMessage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("BookingCancelationMessages");
                });

            modelBuilder.Entity("InitialProject.Model.BookingDelaymentRequest", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("newArrival")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("newDeparture")
                        .HasColumnType("TEXT");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("bookingId");

                    b.ToTable("BookingDelaymentRequests");
                });

            modelBuilder.Entity("InitialProject.Model.CanceledBooking", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("plannedArrival")
                        .HasColumnType("TEXT");

                    b.Property<bool>("seen")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("CanceledBookings");
                });

            modelBuilder.Entity("InitialProject.Model.Coupon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("exiresOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("InitialProject.Model.DelayedBookings", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("previousArrival")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("DelayedBookings");
                });

            modelBuilder.Entity("InitialProject.Model.GuestRate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("cleanness")
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("overallRating")
                        .HasColumnType("REAL");

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

                    b.Property<int?>("AccommodationRateid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Accommodationid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("imageLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("tourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("AccommodationRateid");

                    b.HasIndex("Accommodationid");

                    b.HasIndex("tourId");

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

                    b.Property<bool>("active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("finished")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guideId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("hoursDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("language")
                        .HasColumnType("INTEGER");

                    b.Property<int>("location")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("startDates")
                        .HasColumnType("TEXT");

                    b.Property<int>("touristLimit")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.TourAndGuideRate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("contentRating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guideKnowledgeRating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guideLanguageUsageRating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("personalComment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("tourGuideId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("valid")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourAndGuideRates");
                });

            modelBuilder.Entity("InitialProject.Model.TourAttendance", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("keyPointId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numberOfGuests")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourAttendances");
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

            modelBuilder.Entity("InitialProject.Model.TourMessage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("keyPointId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("numberOfGuests")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourMessages");
                });

            modelBuilder.Entity("InitialProject.Model.TourRequest", b =>
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

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("language")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numberOfTourists")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourRequests");
                });

            modelBuilder.Entity("InitialProject.Model.TourReservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("guestJoined")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("guideConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("withVoucher")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourReservations");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.AnnualAccommodationTransfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("accommodationName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("maxNumOfGuests")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("AccommodationAnnualStatisticsTransfer");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.BookingTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("bookingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("guestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SelectedRatingNotificationTransfer");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.DetailedTourViewTransfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("detailedTourViewTransfers");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.MonthlyAccommodationTransfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("selectedYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("AccommodationsMonthlyStatisticsTransfer");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.TourBookingTransfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cityLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("countryLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("hoursDuration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("keypoints")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("language")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("numberOfGuests")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("startDates")
                        .HasColumnType("TEXT");

                    b.Property<int>("touristLimit")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("tourBookingTransfers");
                });

            modelBuilder.Entity("InitialProject.Model.TransferModels.TourLiveViewTransfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("TourLiveViewTransfers");
                });

            modelBuilder.Entity("InitialProject.Model.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

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

            modelBuilder.Entity("InitialProject.Model.Booking", b =>
                {
                    b.HasOne("InitialProject.Model.Accommodation", null)
                        .WithMany()
                        .HasForeignKey("accommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InitialProject.Model.BookingDelaymentRequest", b =>
                {
                    b.HasOne("InitialProject.Model.Booking", null)
                        .WithMany()
                        .HasForeignKey("bookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InitialProject.Model.Image", b =>
                {
                    b.HasOne("InitialProject.Model.AccommodationRate", null)
                        .WithMany("images")
                        .HasForeignKey("AccommodationRateid");

                    b.HasOne("InitialProject.Model.Accommodation", null)
                        .WithMany("imageLinks")
                        .HasForeignKey("Accommodationid");

                    b.HasOne("InitialProject.Model.Tour", null)
                        .WithMany("imageLinks")
                        .HasForeignKey("tourId");
                });

            modelBuilder.Entity("InitialProject.Model.KeyPoint", b =>
                {
                    b.HasOne("InitialProject.Model.Tour", null)
                        .WithMany("keyPoints")
                        .HasForeignKey("tourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.Navigation("imageLinks");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationRate", b =>
                {
                    b.Navigation("images");
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
