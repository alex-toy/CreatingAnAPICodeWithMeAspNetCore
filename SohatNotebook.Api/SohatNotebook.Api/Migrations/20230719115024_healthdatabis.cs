using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class healthdatabis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("69dc5b78-944f-447a-8969-8e5ca7c9cf72"));

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Address", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "Hobby", "IdentityId", "LastName", "MobileNumber", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("2d433337-e8ec-49fe-9b84-0b490db3249d"), new DateTime(2023, 7, 19, 11, 50, 24, 137, DateTimeKind.Utc).AddTicks(9079), null, "france", new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", null, "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", null, "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("2d433337-e8ec-49fe-9b84-0b490db3249d"));

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Address", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "Hobby", "IdentityId", "LastName", "MobileNumber", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("69dc5b78-944f-447a-8969-8e5ca7c9cf72"), new DateTime(2023, 7, 19, 11, 37, 20, 200, DateTimeKind.Utc).AddTicks(6544), null, "france", new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", null, "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", null, "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
