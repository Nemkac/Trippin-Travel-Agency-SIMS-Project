using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_accommodationId",
                table: "Bookings",
                column: "accommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDelaymentRequests_bookingId",
                table: "BookingDelaymentRequests",
                column: "bookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDelaymentRequests_Bookings_bookingId",
                table: "BookingDelaymentRequests",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Accommodations_accommodationId",
                table: "Bookings",
                column: "accommodationId",
                principalTable: "Accommodations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDelaymentRequests_Bookings_bookingId",
                table: "BookingDelaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Accommodations_accommodationId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_accommodationId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingDelaymentRequests_bookingId",
                table: "BookingDelaymentRequests");
        }
    }
}
