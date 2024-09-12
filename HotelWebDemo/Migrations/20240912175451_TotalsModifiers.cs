using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class TotalsModifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckinInfos_RoomReservations_RoomReservationId",
                table: "CheckinInfos");

            migrationBuilder.DropTable(
                name: "BookingTotalsDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_CheckinInfos_RoomReservationId",
                table: "CheckinInfos");

            migrationBuilder.DropColumn(
                name: "RoomReservationId",
                table: "CheckinInfos");

            migrationBuilder.DropColumn(
                name: "RoomFeaturesPrice",
                table: "BookingTotals");

            migrationBuilder.DropColumn(
                name: "RoomsPrice",
                table: "BookingTotals");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "BookingTotals");

            migrationBuilder.AddColumn<int>(
                name: "BookingItemId",
                table: "RoomReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CheckinInfoId",
                table: "RoomReservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "BookingTotals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TotalsModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsPricePerNight = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalsModifiers_BookingTotals_TotalsId",
                        column: x => x.TotalsId,
                        principalTable: "BookingTotals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TotalsCategoryModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsCategoryModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalsCategoryModifiers_RoomCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "RoomCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TotalsCategoryModifiers_TotalsModifiers_Id",
                        column: x => x.Id,
                        principalTable: "TotalsModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalsDiscountModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsDiscountModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalsDiscountModifiers_TotalsModifiers_Id",
                        column: x => x.Id,
                        principalTable: "TotalsModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalsFeatureModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsFeatureModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalsFeatureModifiers_RoomFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "RoomFeatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TotalsFeatureModifiers_TotalsModifiers_Id",
                        column: x => x.Id,
                        principalTable: "TotalsModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalsTaxModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsTaxModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalsTaxModifiers_TotalsModifiers_Id",
                        column: x => x.Id,
                        principalTable: "TotalsModifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_BookingItemId",
                table: "RoomReservations",
                column: "BookingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_CheckinInfoId",
                table: "RoomReservations",
                column: "CheckinInfoId",
                unique: true,
                filter: "[CheckinInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TotalsCategoryModifiers_CategoryId",
                table: "TotalsCategoryModifiers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TotalsFeatureModifiers_FeatureId",
                table: "TotalsFeatureModifiers",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TotalsModifiers_TotalsId",
                table: "TotalsModifiers",
                column: "TotalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_BookingItems_BookingItemId",
                table: "RoomReservations",
                column: "BookingItemId",
                principalTable: "BookingItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_CheckinInfos_CheckinInfoId",
                table: "RoomReservations",
                column: "CheckinInfoId",
                principalTable: "CheckinInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_BookingItems_BookingItemId",
                table: "RoomReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_CheckinInfos_CheckinInfoId",
                table: "RoomReservations");

            migrationBuilder.DropTable(
                name: "TotalsCategoryModifiers");

            migrationBuilder.DropTable(
                name: "TotalsDiscountModifiers");

            migrationBuilder.DropTable(
                name: "TotalsFeatureModifiers");

            migrationBuilder.DropTable(
                name: "TotalsTaxModifiers");

            migrationBuilder.DropTable(
                name: "TotalsModifiers");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservations_BookingItemId",
                table: "RoomReservations");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservations_CheckinInfoId",
                table: "RoomReservations");

            migrationBuilder.DropColumn(
                name: "BookingItemId",
                table: "RoomReservations");

            migrationBuilder.DropColumn(
                name: "CheckinInfoId",
                table: "RoomReservations");

            migrationBuilder.DropColumn(
                name: "Nights",
                table: "BookingTotals");

            migrationBuilder.AddColumn<int>(
                name: "RoomReservationId",
                table: "CheckinInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomFeaturesPrice",
                table: "BookingTotals",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomsPrice",
                table: "BookingTotals",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "BookingTotals",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BookingTotalsDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalsId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
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
                name: "IX_CheckinInfos_RoomReservationId",
                table: "CheckinInfos",
                column: "RoomReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTotalsDiscounts_TotalsId",
                table: "BookingTotalsDiscounts",
                column: "TotalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckinInfos_RoomReservations_RoomReservationId",
                table: "CheckinInfos",
                column: "RoomReservationId",
                principalTable: "RoomReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
