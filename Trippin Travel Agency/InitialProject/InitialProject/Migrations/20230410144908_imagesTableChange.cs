using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class imagesTableChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tour_Tourid",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Tourid",
                table: "Images",
                newName: "tourId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Tourid",
                table: "Images",
                newName: "IX_Images_tourId");

            migrationBuilder.AlterColumn<int>(
                name: "tourId",
                table: "Images",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tour_tourId",
                table: "Images",
                column: "tourId",
                principalTable: "Tour",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tour_tourId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "tourId",
                table: "Images",
                newName: "Tourid");

            migrationBuilder.RenameIndex(
                name: "IX_Images_tourId",
                table: "Images",
                newName: "IX_Images_Tourid");

            migrationBuilder.AlterColumn<int>(
                name: "Tourid",
                table: "Images",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tour_Tourid",
                table: "Images",
                column: "Tourid",
                principalTable: "Tour",
                principalColumn: "id");
        }
    }
}
