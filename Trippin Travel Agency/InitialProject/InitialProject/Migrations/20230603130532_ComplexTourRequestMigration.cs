using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class ComplexTourRequestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplexTourRequestid",
                table: "TourRequests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComplexTourRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexTourRequests", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourRequests_ComplexTourRequestid",
                table: "TourRequests",
                column: "ComplexTourRequestid");

            migrationBuilder.AddForeignKey(
                name: "FK_TourRequests_ComplexTourRequests_ComplexTourRequestid",
                table: "TourRequests",
                column: "ComplexTourRequestid",
                principalTable: "ComplexTourRequests",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRequests_ComplexTourRequests_ComplexTourRequestid",
                table: "TourRequests");

            migrationBuilder.DropTable(
                name: "ComplexTourRequests");

            migrationBuilder.DropIndex(
                name: "IX_TourRequests_ComplexTourRequestid",
                table: "TourRequests");

            migrationBuilder.DropColumn(
                name: "ComplexTourRequestid",
                table: "TourRequests");
        }
    }
}
