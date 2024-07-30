using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class TurnOffIndexEntityKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Indexed_RoomFeatures",
                table: "Indexed_RoomFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indexed_Hotels",
                table: "Indexed_Hotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Indexed_RoomFeatures",
                table: "Indexed_RoomFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indexed_Hotels",
                table: "Indexed_Hotels",
                column: "Id");
        }
    }
}
