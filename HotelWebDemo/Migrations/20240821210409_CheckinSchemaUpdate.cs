using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class CheckinSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "CustomerAccounts");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "Bookings",
                newName: "CheckoutDate");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "Bookings",
                newName: "CheckinDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "CustomerIdentities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "CustomerIdentities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CheckinInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomReservationId = table.Column<int>(type: "int", nullable: false),
                    CheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckinInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckinInfos_RoomReservations_RoomReservationId",
                        column: x => x.RoomReservationId,
                        principalTable: "RoomReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCheckinInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CheckinInfoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCheckinInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCheckinInfos_CheckinInfos_CheckinInfoId",
                        column: x => x.CheckinInfoId,
                        principalTable: "CheckinInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCheckinInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings",
                column: "ContactId",
                unique: true,
                filter: "[ContactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinInfos_RoomReservationId",
                table: "CheckinInfos",
                column: "RoomReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCheckinInfos_CheckinInfoId",
                table: "CustomerCheckinInfos",
                column: "CheckinInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCheckinInfos_CustomerId",
                table: "CustomerCheckinInfos",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCheckinInfos");

            migrationBuilder.DropTable(
                name: "CheckinInfos");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "CustomerIdentities");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "CustomerIdentities");

            migrationBuilder.RenameColumn(
                name: "CheckoutDate",
                table: "Bookings",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "CheckinDate",
                table: "Bookings",
                newName: "CheckInDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "CustomerAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BookingCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingCustomers_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ContactId",
                table: "Bookings",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomers_BookingId",
                table: "BookingCustomers",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomers_CustomerId",
                table: "BookingCustomers",
                column: "CustomerId");
        }
    }
}
