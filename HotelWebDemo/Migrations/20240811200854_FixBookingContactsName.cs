using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingContactsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingContact_ContactId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingContact",
                table: "BookingContact");

            migrationBuilder.RenameTable(
                name: "BookingContact",
                newName: "BookingContacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingContacts",
                table: "BookingContacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingContacts_ContactId",
                table: "Bookings",
                column: "ContactId",
                principalTable: "BookingContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingContacts_ContactId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingContacts",
                table: "BookingContacts");

            migrationBuilder.RenameTable(
                name: "BookingContacts",
                newName: "BookingContact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingContact",
                table: "BookingContact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingContact_ContactId",
                table: "Bookings",
                column: "ContactId",
                principalTable: "BookingContact",
                principalColumn: "Id");
        }
    }
}
