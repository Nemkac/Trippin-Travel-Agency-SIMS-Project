using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeypointTOurid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints");

            migrationBuilder.RenameColumn(
                name: "Tourid",
                table: "KeyPoints",
                newName: "TourID");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoints_Tourid",
                table: "KeyPoints",
                newName: "IX_KeyPoints_TourID");

            migrationBuilder.AlterColumn<int>(
                name: "TourID",
                table: "KeyPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoints_Tour_TourID",
                table: "KeyPoints",
                column: "TourID",
                principalTable: "Tour",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoints_Tour_TourID",
                table: "KeyPoints");

            migrationBuilder.RenameColumn(
                name: "TourID",
                table: "KeyPoints",
                newName: "Tourid");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoints_TourID",
                table: "KeyPoints",
                newName: "IX_KeyPoints_Tourid");

            migrationBuilder.AlterColumn<int>(
                name: "Tourid",
                table: "KeyPoints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints",
                column: "Tourid",
                principalTable: "Tour",
                principalColumn: "id");
        }
    }
}
