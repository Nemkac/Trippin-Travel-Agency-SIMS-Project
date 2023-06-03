using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class ForumCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    locationId = table.Column<int>(type: "INTEGER", nullable: false),
                    seen = table.Column<bool>(type: "INTEGER", nullable: false),
                    forumId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumMessages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    locationid = table.Column<int>(type: "INTEGER", nullable: false),
                    creatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    isVeryUseful = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.id);
                    table.ForeignKey(
                        name: "FK_Forums_AccommodationLocation_locationid",
                        column: x => x.locationid,
                        principalTable: "AccommodationLocation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumComments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    comment = table.Column<string>(type: "TEXT", nullable: false),
                    postingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    numberOfReports = table.Column<int>(type: "INTEGER", nullable: false),
                    hasGuestVisited = table.Column<bool>(type: "INTEGER", nullable: false),
                    Forumid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumComments", x => x.id);
                    table.ForeignKey(
                        name: "FK_ForumComments_Forums_Forumid",
                        column: x => x.Forumid,
                        principalTable: "Forums",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumComments_Forumid",
                table: "ForumComments",
                column: "Forumid");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_locationid",
                table: "Forums",
                column: "locationid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumComments");

            migrationBuilder.DropTable(
                name: "ForumMessages");

            migrationBuilder.DropTable(
                name: "Forums");
        }
    }
}
