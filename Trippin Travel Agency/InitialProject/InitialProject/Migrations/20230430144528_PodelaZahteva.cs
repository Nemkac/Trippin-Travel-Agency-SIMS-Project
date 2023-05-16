using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class PodelaZahteva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnfulfilledTourCities");

            migrationBuilder.DropTable(
                name: "unfulfilledTourCountries");

            migrationBuilder.DropTable(
                name: "UnfulfilledTourLanguages");
        }
    }
}
