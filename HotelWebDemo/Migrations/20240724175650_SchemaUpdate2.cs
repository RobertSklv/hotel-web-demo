using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "RoomFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RoomFeatures",
                type: "decimal(19,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFeatures_HotelId",
                table: "RoomFeatures",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatures_Hotels_HotelId",
                table: "RoomFeatures",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatures_Hotels_HotelId",
                table: "RoomFeatures");

            migrationBuilder.DropIndex(
                name: "IX_RoomFeatures_HotelId",
                table: "RoomFeatures");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "RoomFeatures");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RoomFeatures");
        }
    }
}
