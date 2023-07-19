using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class update_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("089425fa-0ba1-40c5-8466-df892de53325"));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserDb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserDb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "UserDb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Address", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "Hobby", "IdentityId", "LastName", "MobileNumber", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("a593056e-76d6-423f-b158-932aff0ee9d6"), new DateTime(2023, 7, 19, 5, 47, 4, 275, DateTimeKind.Utc).AddTicks(9437), null, "france", new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", null, "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", null, "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("a593056e-76d6-423f-b158-932aff0ee9d6"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserDb");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserDb");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "UserDb");

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "Hobby", "IdentityId", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("089425fa-0ba1-40c5-8466-df892de53325"), new DateTime(2023, 7, 18, 7, 31, 51, 225, DateTimeKind.Utc).AddTicks(1392), "france", new DateTime(2023, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
