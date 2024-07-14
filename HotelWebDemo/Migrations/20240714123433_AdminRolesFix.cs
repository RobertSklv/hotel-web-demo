using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class AdminRolesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminRoles_Hotels_HotelId",
                table: "AdminRoles");

            migrationBuilder.DropIndex(
                name: "IX_AdminRoles_HotelId",
                table: "AdminRoles");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "AdminRoles");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "AdminUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AdminRoles",
                columns: new[] { "Id", "Code", "CreatedAt", "DisplayedName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "CustomerService", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer Service", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_HotelId",
                table: "AdminUsers",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Hotels_HotelId",
                table: "AdminUsers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Hotels_HotelId",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_HotelId",
                table: "AdminUsers");

            migrationBuilder.DeleteData(
                table: "AdminRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AdminRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "AdminUsers");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "AdminRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoles_HotelId",
                table: "AdminRoles",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminRoles_Hotels_HotelId",
                table: "AdminRoles",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
