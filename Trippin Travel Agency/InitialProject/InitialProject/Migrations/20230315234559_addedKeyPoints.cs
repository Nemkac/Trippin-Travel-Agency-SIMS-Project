using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class addedKeyPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoint_Tour_Tourid",
                table: "KeyPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyPoint",
                table: "KeyPoint");

            migrationBuilder.RenameTable(
                name: "KeyPoint",
                newName: "KeyPoints");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoint_Tourid",
                table: "KeyPoints",
                newName: "IX_KeyPoints_Tourid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyPoints",
                table: "KeyPoints",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints",
                column: "Tourid",
                principalTable: "Tour",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyPoints",
                table: "KeyPoints");

            migrationBuilder.RenameTable(
                name: "KeyPoints",
                newName: "KeyPoint");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoints_Tourid",
                table: "KeyPoint",
                newName: "IX_KeyPoint_Tourid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyPoint",
                table: "KeyPoint",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoint_Tour_Tourid",
                table: "KeyPoint",
                column: "Tourid",
                principalTable: "Tour",
                principalColumn: "id");
        }
    }
}
