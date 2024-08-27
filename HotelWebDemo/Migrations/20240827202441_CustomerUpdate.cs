using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class CustomerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccounts_Addresses_AddressId",
                table: "CustomerAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerIdentities_CustomerIdentityId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "CustomerIdentities");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerIdentityId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccounts_AddressId",
                table: "CustomerAccounts");

            migrationBuilder.DropColumn(
                name: "CustomerIdentityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "CustomerAccounts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerAccounts");

            migrationBuilder.DropColumn(
                name: "CustomerAccountId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CitizenshipId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Customers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportId",
                table: "Customers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CitizenshipId",
                table: "Customers",
                column: "CitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Countries_CitizenshipId",
                table: "Customers",
                column: "CitizenshipId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CustomerAccounts_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "CustomerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Countries_CitizenshipId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CustomerAccounts_AuthorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CitizenshipId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CitizenshipId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerIdentityId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "CustomerAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerAccountId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenshipId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PassportId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerIdentities_Countries_CitizenshipId",
                        column: x => x.CitizenshipId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerIdentityId",
                table: "Customers",
                column: "CustomerIdentityId",
                unique: true,
                filter: "[CustomerIdentityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_AddressId",
                table: "CustomerAccounts",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerIdentities_CitizenshipId",
                table: "CustomerIdentities",
                column: "CitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccounts_Addresses_AddressId",
                table: "CustomerAccounts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerIdentities_CustomerIdentityId",
                table: "Customers",
                column: "CustomerIdentityId",
                principalTable: "CustomerIdentities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
