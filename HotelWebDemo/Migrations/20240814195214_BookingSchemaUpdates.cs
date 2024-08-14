using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class BookingSchemaUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPayments_BookingCancellations_BookingCancellationId",
                table: "BookingPayments");

            migrationBuilder.DropIndex(
                name: "IX_BookingPayments_BookingCancellationId",
                table: "BookingPayments");

            migrationBuilder.DropColumn(
                name: "BookingCancellationId",
                table: "BookingPayments");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingPayments");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "BookingPayments");

            migrationBuilder.AddColumn<int>(
                name: "BookingCancellationId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "Bookings",
                type: "VARCHAR(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalsId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StreetLine3",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "StreetLine2",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookingItems",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "BookingCancellations",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BookingEventLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingEventLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingEventLogs_AdminUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingEventLogs_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingTotals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomsPrice = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    RoomFeaturesPrice = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    CustomGrandTotal = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTotals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingTotalsDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTotalsDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingTotalsDiscounts_BookingTotals_TotalsId",
                        column: x => x.TotalsId,
                        principalTable: "BookingTotals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings",
                column: "BookingCancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TotalsId",
                table: "Bookings",
                column: "TotalsId",
                unique: true,
                filter: "[TotalsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCancellations_BookingPaymentId",
                table: "BookingCancellations",
                column: "BookingPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEventLogs_AdminId",
                table: "BookingEventLogs",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEventLogs_BookingId",
                table: "BookingEventLogs",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTotalsDiscounts_TotalsId",
                table: "BookingTotalsDiscounts",
                column: "TotalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCancellations_BookingPayments_BookingPaymentId",
                table: "BookingCancellations",
                column: "BookingPaymentId",
                principalTable: "BookingPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingCancellations_BookingCancellationId",
                table: "Bookings",
                column: "BookingCancellationId",
                principalTable: "BookingCancellations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingTotals_TotalsId",
                table: "Bookings",
                column: "TotalsId",
                principalTable: "BookingTotals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCancellations_BookingPayments_BookingPaymentId",
                table: "BookingCancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingCancellations_BookingCancellationId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingTotals_TotalsId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingEventLogs");

            migrationBuilder.DropTable(
                name: "BookingTotalsDiscounts");

            migrationBuilder.DropTable(
                name: "BookingTotals");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingCancellationId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TotalsId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingCancellations_BookingPaymentId",
                table: "BookingCancellations");

            migrationBuilder.DropColumn(
                name: "BookingCancellationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalsId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BookingPayments");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "BookingPayments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingItems");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "BookingCancellations");

            migrationBuilder.AlterColumn<string>(
                name: "StreetLine3",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StreetLine2",
                table: "BookingPayments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingCancellationId",
                table: "BookingPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "BookingPayments",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_BookingPayments_BookingCancellationId",
                table: "BookingPayments",
                column: "BookingCancellationId",
                unique: true,
                filter: "[BookingCancellationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPayments_BookingCancellations_BookingCancellationId",
                table: "BookingPayments",
                column: "BookingCancellationId",
                principalTable: "BookingCancellations",
                principalColumn: "Id");
        }
    }
}
