using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConstraintFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomers_Bookings_BookingId",
                table: "BookingCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomers_Customers_CustomerId",
                table: "BookingCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_BookingItems_BookingItemId",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItems_Bookings_BookingId",
                table: "BookingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItems_RoomCategories_RoomCategoryId",
                table: "BookingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPayments_Countries_CountryId",
                table: "BookingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPayments_Customers_CustomerId",
                table: "BookingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_Rooms_RoomId",
                table: "RoomFeatureRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomers_Bookings_BookingId",
                table: "BookingCustomers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomers_Customers_CustomerId",
                table: "BookingCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_BookingItems_BookingItemId",
                table: "BookingItemRoomFeatures",
                column: "BookingItemId",
                principalTable: "BookingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItems_Bookings_BookingId",
                table: "BookingItems",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItems_RoomCategories_RoomCategoryId",
                table: "BookingItems",
                column: "RoomCategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPayments_Countries_CountryId",
                table: "BookingPayments",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPayments_Customers_CustomerId",
                table: "BookingPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_Rooms_RoomId",
                table: "RoomFeatureRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomers_Bookings_BookingId",
                table: "BookingCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCustomers_Customers_CustomerId",
                table: "BookingCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_BookingItems_BookingItemId",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItems_Bookings_BookingId",
                table: "BookingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingItems_RoomCategories_RoomCategoryId",
                table: "BookingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPayments_Countries_CountryId",
                table: "BookingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPayments_Customers_CustomerId",
                table: "BookingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatureRooms_Rooms_RoomId",
                table: "RoomFeatureRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomers_Bookings_BookingId",
                table: "BookingCustomers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCustomers_Customers_CustomerId",
                table: "BookingCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_BookingItems_BookingItemId",
                table: "BookingItemRoomFeatures",
                column: "BookingItemId",
                principalTable: "BookingItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItemRoomFeatures_RoomFeatures_RoomFeatureId",
                table: "BookingItemRoomFeatures",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItems_Bookings_BookingId",
                table: "BookingItems",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingItems_RoomCategories_RoomCategoryId",
                table: "BookingItems",
                column: "RoomCategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPayments_Countries_CountryId",
                table: "BookingPayments",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPayments_Customers_CustomerId",
                table: "BookingPayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_RoomFeatures_RoomFeatureId",
                table: "RoomFeatureRooms",
                column: "RoomFeatureId",
                principalTable: "RoomFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatureRooms_Rooms_RoomId",
                table: "RoomFeatureRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
