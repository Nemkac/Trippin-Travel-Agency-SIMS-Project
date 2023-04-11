using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class prvenac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tour_tourId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "tourId",
                table: "Images",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "BookingCancelationMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCancelationMessages", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tour_tourId",
                table: "Images",
                column: "tourId",
                principalTable: "Tour",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tour_tourId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "BookingCancelationMessages");

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
    }
}
