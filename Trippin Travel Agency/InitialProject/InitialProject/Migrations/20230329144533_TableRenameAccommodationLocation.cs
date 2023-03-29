using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class TableRenameAccommodationLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationid",
                table: "Accommodations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationsOfAccommodations",
                table: "LocationsOfAccommodations");

            migrationBuilder.RenameTable(
                name: "LocationsOfAccommodations",
                newName: "AccommodationLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationLocation",
                table: "AccommodationLocation",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_AccommodationLocation_locationid",
                table: "Accommodations",
                column: "locationid",
                principalTable: "AccommodationLocation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_AccommodationLocation_locationid",
                table: "Accommodations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationLocation",
                table: "AccommodationLocation");

            migrationBuilder.RenameTable(
                name: "AccommodationLocation",
                newName: "LocationsOfAccommodations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationsOfAccommodations",
                table: "LocationsOfAccommodations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationid",
                table: "Accommodations",
                column: "locationid",
                principalTable: "LocationsOfAccommodations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
