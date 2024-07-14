using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelWebDemo.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "AF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afghanistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "AX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�land Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "AL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Albania", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "DZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Algeria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "AS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "American Samoa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "AD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andorra", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "AO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angola", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "AI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anguilla", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "AQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antarctica", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "AG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antigua and Barbuda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "AR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Argentina", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "AM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Armenia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "AW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aruba", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "AU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Australia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "AT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Austria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "AZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Azerbaijan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "BS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahamas", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "BH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahrain", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "BD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangladesh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "BB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barbados", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "BY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belarus", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "BE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belgium", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "BZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belize", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "BJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Benin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "BM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bermuda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "BT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bhutan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "BO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bolivia (Plurinational State of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "BQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bonaire, Sint Eustatius and Saba", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bosnia and Herzegovina", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "BW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Botswana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "BV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bouvet Island", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "BR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brazil", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "IO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "British Indian Ocean Territory", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "UM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United States Minor Outlying Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "VG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgin Islands (British)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "VI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgin Islands (U.S.)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "BN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brunei Darussalam", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "BG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bulgaria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "BF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burkina Faso", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "BI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burundi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "KH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cambodia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "CM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cameroon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "CA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canada", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "CV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cabo Verde", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "KY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cayman Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "CF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Central African Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "TD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chad", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "CL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chile", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "CN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "CX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Island", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, "CC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cocos (Keeling) Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, "CO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colombia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, "KM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comoros", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, "CG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Congo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, "CD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Congo (Democratic Republic of the)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, "CK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cook Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, "CR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Costa Rica", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, "HR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Croatia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, "CU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cuba", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, "CW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cura�ao", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, "CY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyprus", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, "CZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Czech Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, "DK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denmark", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, "DJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Djibouti", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, "DM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominica", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, "DO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominican Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, "EC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ecuador", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, "EG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, "SV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "El Salvador", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, "GQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equatorial Guinea", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, "ER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eritrea", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, "EE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estonia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, "ET", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ethiopia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, "FK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Falkland Islands (Malvinas)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, "FO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faroe Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, "FJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fiji", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, "FI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, "FR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "France", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, "GF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Guiana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, "PF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Polynesia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, "TF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Southern Territories", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, "GA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, "GM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gambia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, "GE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Georgia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, "DE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Germany", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, "GH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, "GI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gibraltar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, "GR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greece", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, "GL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greenland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, "GD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grenada", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, "GP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guadeloupe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, "GU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guam", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, "GT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guatemala", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, "GG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guernsey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, "GN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guinea", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, "GW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guinea-Bissau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, "GY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guyana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, "HT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Haiti", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, "HM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heard Island and McDonald Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "VA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vatican City", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101, "HN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honduras", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, "HU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hungary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 103, "HK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hong Kong", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 104, "IS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iceland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 105, "IN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "India", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, "ID", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indonesia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 107, "CI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivory Coast", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 108, "IR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iran (Islamic Republic of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 109, "IQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iraq", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 110, "IE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ireland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 111, "IM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Isle of Man", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 112, "IL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Israel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 113, "IT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 114, "JM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jamaica", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 115, "JP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Japan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 116, "JE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jersey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, "JO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jordan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 118, "KZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kazakhstan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 119, "KE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kenya", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 120, "KI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiribati", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 121, "KW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuwait", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 122, "KG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kyrgyzstan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 123, "LA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lao People's Democratic Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 124, "LV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latvia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 125, "LB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lebanon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 126, "LS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lesotho", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 127, "LR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liberia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 128, "LY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libya", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 129, "LI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liechtenstein", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 130, "LT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lithuania", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 131, "LU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxembourg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 132, "MO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macao", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 133, "MK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macedonia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 134, "MG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Madagascar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 135, "MW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malawi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 136, "MY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malaysia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 137, "MV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maldives", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, "ML", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mali", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, "MT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malta", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, "MH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marshall Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 141, "MQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martinique", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 142, "MR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauritania", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 143, "MU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauritius", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 144, "YT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mayotte", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 145, "MX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mexico", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 146, "FM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Micronesia (Federated States of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 147, "MD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moldova (Republic of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, "MC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monaco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 149, "MN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mongolia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 150, "ME", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Montenegro", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 151, "MS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Montserrat", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 152, "MA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Morocco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 153, "MZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mozambique", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 154, "MM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Myanmar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, "NA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Namibia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 156, "NR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nauru", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 157, "NP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nepal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 158, "NL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netherlands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 159, "NC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Caledonia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 160, "NZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Zealand", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 161, "NI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nicaragua", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 162, "NE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Niger", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 163, "NG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nigeria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, "NU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Niue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 165, "NF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Norfolk Island", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, "KP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Korea (Democratic People's Republic of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 167, "MP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Northern Mariana Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 168, "NO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Norway", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 169, "OM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oman", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 170, "PK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pakistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, "PW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 172, "PS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palestine, State of", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 173, "PA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Panama", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 174, "PG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Papua New Guinea", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, "PY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paraguay", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 176, "PE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peru", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 177, "PH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippines", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, "PN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pitcairn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 179, "PL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, "PT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portugal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 181, "PR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Puerto Rico", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 182, "QA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Qatar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 183, "XK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Republic of Kosovo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 184, "RE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "R�union", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 185, "RO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romania", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 186, "RU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Russian Federation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 187, "RW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rwanda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 188, "BL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Barth�lemy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 189, "SH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Helena, Ascension and Tristan da Cunha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 190, "KN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Kitts and Nevis", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 191, "LC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Lucia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 192, "MF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Martin (French part)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 193, "PM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Pierre and Miquelon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 194, "VC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Vincent and the Grenadines", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 195, "WS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samoa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 196, "SM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Marino", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 197, "ST", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sao Tome and Principe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 198, "SA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saudi Arabia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 199, "SN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senegal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 200, "RS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serbia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 201, "SC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seychelles", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 202, "SL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sierra Leone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 203, "SG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singapore", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 204, "SX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sint Maarten (Dutch part)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 205, "SK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slovakia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 206, "SI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slovenia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 207, "SB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solomon Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 208, "SO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Somalia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 209, "ZA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "South Africa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 210, "GS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "South Georgia and the South Sandwich Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 211, "KR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Korea (Republic of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 212, "ES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spain", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 213, "LK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sri Lanka", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 214, "SD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sudan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 215, "SS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "South Sudan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 216, "SR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suriname", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 217, "SJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Svalbard and Jan Mayen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 218, "SZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Swaziland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 219, "SE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sweden", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 220, "CH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Switzerland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 221, "SY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Syrian Arab Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 222, "TW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taiwan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 223, "TJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tajikistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 224, "TZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tanzania, United Republic of", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 225, "TH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thailand", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 226, "TL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Timor-Leste", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 227, "TG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Togo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 228, "TK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tokelau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 229, "TO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tonga", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 230, "TT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trinidad and Tobago", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 231, "TN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tunisia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 232, "TR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 233, "TM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkmenistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 234, "TC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turks and Caicos Islands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 235, "TV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tuvalu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 236, "UG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uganda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 237, "UA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ukraine", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 238, "AE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United Arab Emirates", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 239, "GB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United Kingdom of Great Britain and Northern Ireland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 240, "US", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United States of America", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 241, "UY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uruguay", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 242, "UZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uzbekistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 243, "VU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vanuatu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 244, "VE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venezuela (Bolivarian Republic of)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 245, "VN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vietnam", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 246, "WF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallis and Futuna", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 247, "EH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Western Sahara", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 248, "YE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yemen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 249, "ZM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zambia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 250, "ZW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zimbabwe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 250);
        }
    }
}
