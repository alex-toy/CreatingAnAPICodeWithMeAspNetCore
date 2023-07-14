using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class profession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1a958ef4-f414-498b-ad62-66a052747c9b"));

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("14f7a60c-bef3-4368-82c6-ae5b76a55aba"), new DateTime(2023, 7, 14, 11, 58, 23, 734, DateTimeKind.Utc).AddTicks(372), "france", new DateTime(2023, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("14f7a60c-bef3-4368-82c6-ae5b76a55aba"));

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "User");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "LastName", "Phone", "Status", "UpdateDate" },
                values: new object[] { new Guid("1a958ef4-f414-498b-ad62-66a052747c9b"), new DateTime(2023, 7, 14, 11, 51, 17, 540, DateTimeKind.Utc).AddTicks(4876), "france", new DateTime(2023, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "rea", "1234", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
