using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class AnnualTransferTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accommodationType",
                table: "AccommodationAnnualStatisticsTransfer");

            migrationBuilder.AlterColumn<string>(
                name: "accommodationName",
                table: "AccommodationAnnualStatisticsTransfer",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "accommodationName",
                table: "AccommodationAnnualStatisticsTransfer",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "accommodationType",
                table: "AccommodationAnnualStatisticsTransfer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
