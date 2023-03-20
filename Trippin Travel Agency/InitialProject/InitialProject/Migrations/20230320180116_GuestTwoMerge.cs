﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class GuestTwoMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    arrival = table.Column<string>(type: "TEXT", nullable: false),
                    departure = table.Column<string>(type: "TEXT", nullable: false),
                    stayingPeriod = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
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
                    overallRating = table.Column<int>(type: "INTEGER", nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestRate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LocationsOfAccommodations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsOfAccommodations", x => x.id);
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
                    active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.id);
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
                name: "TourReservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false),
                    tourId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReservations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<string>(type: "TEXT", nullable: false)
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
                    type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accommodations_LocationsOfAccommodations_locationid",
                        column: x => x.locationid,
                        principalTable: "LocationsOfAccommodations",
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
                    Tourid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                    table.ForeignKey(
                        name: "FK_Images_Tour_Tourid",
                        column: x => x.Tourid,
                        principalTable: "Tour",
                        principalColumn: "id");
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

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_locationid",
                table: "Accommodations",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Tourid",
                table: "Images",
                column: "Tourid");

            migrationBuilder.CreateIndex(
                name: "IX_KeyPoints_tourId",
                table: "KeyPoints",
                column: "tourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "GuestRate");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "KeyPoints");

            migrationBuilder.DropTable(
                name: "TourLocation");

            migrationBuilder.DropTable(
                name: "TourReservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LocationsOfAccommodations");

            migrationBuilder.DropTable(
                name: "Tour");
        }
    }
}