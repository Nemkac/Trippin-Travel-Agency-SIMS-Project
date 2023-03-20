using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class addedNemkacImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accommodationid",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Accommodationid",
                table: "Images",
                column: "Accommodationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accommodations_Accommodationid",
                table: "Images",
                column: "Accommodationid",
                principalTable: "Accommodations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accommodations_Accommodationid",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Accommodationid",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Accommodationid",
                table: "Images");
        }
    }
}
