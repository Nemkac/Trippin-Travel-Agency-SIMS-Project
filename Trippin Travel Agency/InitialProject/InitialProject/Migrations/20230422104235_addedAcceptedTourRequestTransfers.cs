using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class addedAcceptedTourRequestTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcceptedTourRequestViewTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    requestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptedTourRequestViewTransfers", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptedTourRequestViewTransfers");
        }
    }
}
