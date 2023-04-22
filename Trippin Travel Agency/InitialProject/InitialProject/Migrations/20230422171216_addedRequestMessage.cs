using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class addedRequestMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    requestId = table.Column<int>(type: "INTEGER", nullable: false),
                    guestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMessages", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestMessages");
        }
    }
}
