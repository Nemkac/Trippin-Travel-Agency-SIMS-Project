using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class AccommodationRateImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccommodationRateid",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccommodationRateid",
                table: "Images",
                column: "AccommodationRateid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AccommodationRates_AccommodationRateid",
                table: "Images",
                column: "AccommodationRateid",
                principalTable: "AccommodationRates",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AccommodationRates_AccommodationRateid",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AccommodationRateid",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AccommodationRateid",
                table: "Images");
        }
    }
}
