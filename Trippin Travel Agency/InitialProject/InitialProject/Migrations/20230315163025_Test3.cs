using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class Test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "LocationsOfAccommodations",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "LocationsOfAccommodations",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LocationsOfAccommodations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Tourid",
                table: "KeyPoints",
                newName: "tourId");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoints_Tourid",
                table: "KeyPoints",
                newName: "IX_KeyPoints_tourId");

            migrationBuilder.RenameColumn(
                name: "locationId",
                table: "Accommodations",
                newName: "locationid");

            migrationBuilder.RenameIndex(
                name: "IX_Accommodations_locationId",
                table: "Accommodations",
                newName: "IX_Accommodations_locationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationid",
                table: "Accommodations",
                column: "locationid",
                principalTable: "LocationsOfAccommodations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoints_Tour_tourId",
                table: "KeyPoints",
                column: "tourId",
                principalTable: "Tour",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationid",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyPoints_Tour_tourId",
                table: "KeyPoints");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "LocationsOfAccommodations",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "LocationsOfAccommodations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "LocationsOfAccommodations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tourId",
                table: "KeyPoints",
                newName: "Tourid");

            migrationBuilder.RenameIndex(
                name: "IX_KeyPoints_tourId",
                table: "KeyPoints",
                newName: "IX_KeyPoints_Tourid");

            migrationBuilder.RenameColumn(
                name: "locationid",
                table: "Accommodations",
                newName: "locationId");

            migrationBuilder.RenameIndex(
                name: "IX_Accommodations_locationid",
                table: "Accommodations",
                newName: "IX_Accommodations_locationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_LocationsOfAccommodations_locationId",
                table: "Accommodations",
                column: "locationId",
                principalTable: "LocationsOfAccommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPoints_Tour_Tourid",
                table: "KeyPoints",
                column: "Tourid",
                principalTable: "Tour",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
