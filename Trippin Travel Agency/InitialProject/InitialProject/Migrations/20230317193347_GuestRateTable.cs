using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class GuestRateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestRate", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_TourLocation_locationid",
                table: "Tour",
                column: "locationid",
                principalTable: "TourLocation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_TourLocation_locationid",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "GuestRate");

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
    }
}
