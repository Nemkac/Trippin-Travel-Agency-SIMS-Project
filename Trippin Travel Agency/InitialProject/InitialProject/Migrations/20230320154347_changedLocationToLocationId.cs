using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class changedLocationToLocationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TourLocation_locationid",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Tour_locationid",
                table: "Tour");

            migrationBuilder.RenameColumn(
                name: "locationid",
                table: "Tour",
                newName: "location");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "location",
                table: "Tour",
                newName: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_locationid",
                table: "Tour",
                column: "locationid");

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
