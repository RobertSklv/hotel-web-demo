using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class RoomFeatureIndexTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureid",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureid",
                table: "RoomFeatureRooms");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureid",
                table: "RoomFeatureRooms",
                newName: "RoomFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFeatureRooms_RoomFeatureid",
                table: "RoomFeatureRooms",
                newName: "IX_RoomFeatureRooms_RoomFeatureId");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureid",
                table: "BookingItemRoomFeatures",
                newName: "RoomFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingItemRoomFeatures_RoomFeatureid",
                table: "BookingItemRoomFeatures",
                newName: "IX_BookingItemRoomFeatures_RoomFeatureId");

            migrationBuilder.CreateTable(
                name: "Indexed_RoomFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    TimesBooked = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indexed_RoomFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indexed_RoomFeatures_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Indexed_RoomFeatures_HotelId",
                table: "Indexed_RoomFeatures",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms");

            migrationBuilder.DropTable(
                name: "Indexed_RoomFeatures");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureId",
                table: "RoomFeatureRooms",
                newName: "RoomFeatureid");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFeatureRooms_RoomFeatureId",
                table: "RoomFeatureRooms",
                newName: "IX_RoomFeatureRooms_RoomFeatureid");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureId",
                table: "BookingItemRoomFeatures",
                newName: "RoomFeatureid");

            migrationBuilder.RenameIndex(
                name: "IX_BookingItemRoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures",
                newName: "IX_BookingItemRoomFeatures_RoomFeatureid");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureid",
                table: "BookingItemRoomFeatures",
                column: "RoomFeatureid",
                principalTable: "RoomFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureid",
                table: "RoomFeatureRooms",
                column: "RoomFeatureid",
                principalTable: "RoomFeatures",
                principalColumn: "Id");
        }
    }
}
