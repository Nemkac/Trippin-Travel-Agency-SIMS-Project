using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class RequestTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedRequestTransfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guestName = table.Column<string>(type: "TEXT", nullable: false),
                    bookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    accommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    oldArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    oldDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    newArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    newDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    possible = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedRequestTransfers", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedRequestTransfers");
        }
    }
}
