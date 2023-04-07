using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedTourReserveBridge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tourBookingTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tourBookingTransfers");
        }
    }
}
