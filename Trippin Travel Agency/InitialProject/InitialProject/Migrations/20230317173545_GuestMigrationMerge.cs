using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class GuestMigrationMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TourLocation_locationid",
                table: "Tour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourLocation",
                table: "TourLocation");

            migrationBuilder.RenameTable(
                name: "TourLocation",
                newName: "TourLocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourLocations",
                table: "TourLocations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_TourLocations_locationid",
                table: "Tour",
                column: "locationid",
                principalTable: "TourLocations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TourLocations_locationid",
                table: "Tour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourLocations",
                table: "TourLocations");

            migrationBuilder.RenameTable(
                name: "TourLocations",
                newName: "TourLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourLocation",
                table: "TourLocation",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_TourLocation_locationid",
                table: "Tour",
                column: "locationid",
                principalTable: "TourLocation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
