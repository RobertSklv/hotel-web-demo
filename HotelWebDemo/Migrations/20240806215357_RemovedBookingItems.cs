using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBookingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingPayments_BookingPaymentId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingPaymentId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "BookingPaymentId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetCapacity",
                table: "BookingItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookingContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingContact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRooms_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingPaymentId",
                table: "Bookings",
                column: "BookingPaymentId",
                unique: true,
                filter: "[BookingPaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_BookingId",
                table: "BookingRooms",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_RoomId",
                table: "BookingRooms",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingContact_ContactId",
                table: "Bookings",
                column: "ContactId",
                principalTable: "BookingContact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingPayments_BookingPaymentId",
                table: "Bookings",
                column: "BookingPaymentId",
                principalTable: "BookingPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingContact_ContactId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingPayments_BookingPaymentId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingContact");

            migrationBuilder.DropTable(
                name: "BookingRooms");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingPaymentId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TargetCapacity",
                table: "BookingItems");

            migrationBuilder.AlterColumn<int>(
                name: "BookingPaymentId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingPaymentId",
                table: "Bookings",
                column: "BookingPaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingPayments_BookingPaymentId",
                table: "Bookings",
                column: "BookingPaymentId",
                principalTable: "BookingPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
