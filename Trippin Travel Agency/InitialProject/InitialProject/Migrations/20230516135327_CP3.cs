using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class CP3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcceptedTourRequestViewTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    requestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptedTourRequestViewTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationAnnualStatisticsTransfer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationName = table.Column<string>(type: "TEXT", nullable: false),
                    location = table.Column<string>(type: "TEXT", nullable: false),
                    maxNumOfGuests = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationAnnualStatisticsTransfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationLocation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationLocation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationRates",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    cleanness = table.Column<int>(type: "INTEGER", nullable: false),
                    ownerRate = table.Column<int>(type: "INTEGER", nullable: false),
                    comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationRenovations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationName = table.Column<string>(type: "TEXT", nullable: false),
                    accommodationType = table.Column<string>(type: "TEXT", nullable: false),
                    startDate = table.Column<string>(type: "TEXT", nullable: false),
                    endDate = table.Column<string>(type: "TEXT", nullable: false),
                    duration = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRenovations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationsMonthlyStatisticsTransfer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    selectedYear = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationsMonthlyStatisticsTransfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BookingCancelationMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCancelationMessages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CanceledBookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    seen = table.Column<bool>(type: "INTEGER", nullable: false),
                    plannedArrival = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanceledBookings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    exiresOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DelayedBookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    previousArrival = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayedBookings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detailedTourViewTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailedTourViewTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GuestRate",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cleanness = table.Column<int>(type: "INTEGER", nullable: false),
                    respectingRules = table.Column<int>(type: "INTEGER", nullable: false),
                    comment = table.Column<string>(type: "TEXT", nullable: false),
                    overallRating = table.Column<float>(type: "REAL", nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestRate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RequestMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    requestId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMessages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedRatingNotificationTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedRatingNotificationTransfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedRequestTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestName = table.Column<string>(type: "TEXT", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    oldArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    oldDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    newArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    newDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    possible = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedRequestTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SuperGuests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    points = table.Column<int>(type: "INTEGER", nullable: false),
                    titleAcquisition = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ifActive = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperGuests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    location = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false),
                    touristLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    startDates = table.Column<DateTime>(type: "TEXT", nullable: false),
                    hoursDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    active = table.Column<bool>(type: "INTEGER", nullable: false),
                    guideId = table.Column<int>(type: "INTEGER", nullable: false),
                    finished = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourAndGuideRates",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    guideKnowledgeRating = table.Column<int>(type: "INTEGER", nullable: false),
                    guideLanguageUsageRating = table.Column<int>(type: "INTEGER", nullable: false),
                    contentRating = table.Column<int>(type: "INTEGER", nullable: false),
                    personalComment = table.Column<string>(type: "TEXT", nullable: false),
                    tourGuideId = table.Column<int>(type: "INTEGER", nullable: false),
                    valid = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourAndGuideRates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourAttendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    keyPointId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestID = table.Column<int>(type: "INTEGER", nullable: false),
                    numberOfGuests = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourAttendances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tourBookingTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    cityLocation = table.Column<string>(type: "TEXT", nullable: false),
                    countryLocation = table.Column<string>(type: "TEXT", nullable: false),
                    keypoints = table.Column<string>(type: "TEXT", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false),
                    touristLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    startDates = table.Column<DateTime>(type: "TEXT", nullable: false),
                    hoursDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    numberOfGuests = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourBookingTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourFlagTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    flag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFlagTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourLanguageTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    language = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLanguageTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourLiveViewTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLiveViewTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourLocation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLocation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourLocationTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLocationTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    keyPointId = table.Column<int>(type: "INTEGER", nullable: false),
                    numberOfGuests = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourMessages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    numberOfTourists = table.Column<int>(type: "INTEGER", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false),
                    startDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    endDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    acceptedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    sent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourReservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    guestJoined = table.Column<bool>(type: "INTEGER", nullable: false),
                    guideConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    withVoucher = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReservations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourStatisticsTransfer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    numberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    tourName = table.Column<string>(type: "TEXT", nullable: false),
                    startDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourStatisticsTransfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TourTodayImagesTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTodayImagesTransfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UnfulfilledTourCities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnfulfilledTourCities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unfulfilledTourCountries",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unfulfilledTourCountries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UnfulfilledTourLanguages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnfulfilledTourLanguages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UnfulfilledTourRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false),
                    fulfilled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnfulfilledTourRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<string>(type: "TEXT", nullable: false),
                    age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    locationid = table.Column<int>(type: "INTEGER", nullable: false),
                    guestLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    minDaysBooked = table.Column<int>(type: "INTEGER", nullable: false),
                    bookingCancelPeriodDays = table.Column<int>(type: "INTEGER", nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    ownerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccommodationLocation_locationid",
                        column: x => x.locationid,
                        principalTable: "AccommodationLocation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyPoints",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    visited = table.Column<bool>(type: "INTEGER", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPoints", x => x.id);
                    table.ForeignKey(
                        name: "FK_KeyPoints_Tour_tourId",
                        column: x => x.tourId,
                        principalTable: "Tour",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    arrival = table.Column<string>(type: "TEXT", nullable: false),
                    departure = table.Column<string>(type: "TEXT", nullable: false),
                    daysToStay = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Accommodations_accommodationId",
                        column: x => x.accommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    imageLink = table.Column<string>(type: "TEXT", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: true),
                    AccommodationRateid = table.Column<int>(type: "INTEGER", nullable: true),
                    Accommodationid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                    table.ForeignKey(
                        name: "FK_Images_AccommodationRates_AccommodationRateid",
                        column: x => x.AccommodationRateid,
                        principalTable: "AccommodationRates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Images_Accommodations_Accommodationid",
                        column: x => x.Accommodationid,
                        principalTable: "Accommodations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Images_Tour_tourId",
                        column: x => x.tourId,
                        principalTable: "Tour",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BookingDelaymentRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    newArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    newDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDelaymentRequests", x => x.id);
                    table.ForeignKey(
                        name: "FK_BookingDelaymentRequests_Bookings_bookingId",
                        column: x => x.bookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_locationid",
                table: "Accommodations",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDelaymentRequests_bookingId",
                table: "BookingDelaymentRequests",
                column: "bookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_accommodationId",
                table: "Bookings",
                column: "accommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Accommodationid",
                table: "Images",
                column: "Accommodationid");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccommodationRateid",
                table: "Images",
                column: "AccommodationRateid");

            migrationBuilder.CreateIndex(
                name: "IX_Images_tourId",
                table: "Images",
                column: "tourId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyPoints_tourId",
                table: "KeyPoints",
                column: "tourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptedTourRequestViewTransfers");

            migrationBuilder.DropTable(
                name: "AccommodationAnnualStatisticsTransfer");

            migrationBuilder.DropTable(
                name: "AccommodationRenovations");

            migrationBuilder.DropTable(
                name: "AccommodationsMonthlyStatisticsTransfer");

            migrationBuilder.DropTable(
                name: "BookingCancelationMessages");

            migrationBuilder.DropTable(
                name: "BookingDelaymentRequests");

            migrationBuilder.DropTable(
                name: "CanceledBookings");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "DelayedBookings");

            migrationBuilder.DropTable(
                name: "detailedTourViewTransfers");

            migrationBuilder.DropTable(
                name: "GuestRate");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "KeyPoints");

            migrationBuilder.DropTable(
                name: "RequestMessages");

            migrationBuilder.DropTable(
                name: "SelectedRatingNotificationTransfer");

            migrationBuilder.DropTable(
                name: "SelectedRequestTransfers");

            migrationBuilder.DropTable(
                name: "SuperGuests");

            migrationBuilder.DropTable(
                name: "TourAndGuideRates");

            migrationBuilder.DropTable(
                name: "TourAttendances");

            migrationBuilder.DropTable(
                name: "tourBookingTransfers");

            migrationBuilder.DropTable(
                name: "TourFlagTransfers");

            migrationBuilder.DropTable(
                name: "TourLanguageTransfers");

            migrationBuilder.DropTable(
                name: "TourLiveViewTransfers");

            migrationBuilder.DropTable(
                name: "TourLocation");

            migrationBuilder.DropTable(
                name: "TourLocationTransfers");

            migrationBuilder.DropTable(
                name: "TourMessages");

            migrationBuilder.DropTable(
                name: "TourRequests");

            migrationBuilder.DropTable(
                name: "TourReservations");

            migrationBuilder.DropTable(
                name: "TourStatisticsTransfer");

            migrationBuilder.DropTable(
                name: "TourTodayImagesTransfers");

            migrationBuilder.DropTable(
                name: "UnfulfilledTourCities");

            migrationBuilder.DropTable(
                name: "unfulfilledTourCountries");

            migrationBuilder.DropTable(
                name: "UnfulfilledTourLanguages");

            migrationBuilder.DropTable(
                name: "UnfulfilledTourRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AccommodationRates");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "AccommodationLocation");
        }
    }
}
