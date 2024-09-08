using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class BookingEventLogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingEventLogs_AdminUsers_AdminId",
                table: "BookingEventLogs");

            migrationBuilder.DropIndex(
                name: "IX_BookingEventLogs_AdminId",
                table: "BookingEventLogs");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "BookingEventLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "BookingEventLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookingEventLogs_AdminId",
                table: "BookingEventLogs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingEventLogs_AdminUsers_AdminId",
                table: "BookingEventLogs",
                column: "AdminId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
