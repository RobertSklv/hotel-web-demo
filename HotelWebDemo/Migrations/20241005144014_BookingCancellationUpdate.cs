using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class BookingCancellationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCancellations_BookingPayments_BookingPaymentId",
                table: "BookingCancellations");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingCancellations_BookingPaymentId",
                table: "BookingCancellations");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingPaymentId",
                table: "BookingCancellations");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "BookingCancellations");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings",
                column: "BookingCancellationId",
                unique: true,
                filter: "[BookingCancellationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "Bookings",
                type: "VARCHAR(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookingPaymentId",
                table: "BookingCancellations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "BookingCancellations",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings",
                column: "BookingCancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCancellations_BookingPaymentId",
                table: "BookingCancellations",
                column: "BookingPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCancellations_BookingPayments_BookingPaymentId",
                table: "BookingCancellations",
                column: "BookingPaymentId",
                principalTable: "BookingPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
