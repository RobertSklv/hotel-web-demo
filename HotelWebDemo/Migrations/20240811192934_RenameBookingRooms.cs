using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class RenameBookingRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingRooms_Rooms_RoomId",
                table: "BookingRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingRooms",
                table: "BookingRooms");

            migrationBuilder.RenameTable(
                name: "BookingRooms",
                newName: "RoomReservations");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRooms_RoomId",
                table: "RoomReservations",
                newName: "IX_RoomReservations_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRooms_BookingId",
                table: "RoomReservations",
                newName: "IX_RoomReservations_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomReservations",
                table: "RoomReservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_Bookings_BookingId",
                table: "RoomReservations",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_Rooms_RoomId",
                table: "RoomReservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_Bookings_BookingId",
                table: "RoomReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_Rooms_RoomId",
                table: "RoomReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomReservations",
                table: "RoomReservations");

            migrationBuilder.RenameTable(
                name: "RoomReservations",
                newName: "BookingRooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomReservations_RoomId",
                table: "BookingRooms",
                newName: "IX_BookingRooms_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomReservations_BookingId",
                table: "BookingRooms",
                newName: "IX_BookingRooms_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingRooms",
                table: "BookingRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRooms_Rooms_RoomId",
                table: "BookingRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
