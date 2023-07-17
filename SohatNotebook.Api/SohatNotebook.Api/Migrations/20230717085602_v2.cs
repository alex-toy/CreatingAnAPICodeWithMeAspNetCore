using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("c5b9e5dc-b5a2-49eb-832d-5b1dae1c6b36"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                table: "UserDb",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "Hobby", "IdentityId", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("a4d897e2-d6e8-444d-8c68-0e2fd9c3e749"), new DateTime(2023, 7, 17, 8, 56, 2, 596, DateTimeKind.Utc).AddTicks(5247), "france", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("a4d897e2-d6e8-444d-8c68-0e2fd9c3e749"));

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "UserDb");

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "Hobby", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("c5b9e5dc-b5a2-49eb-832d-5b1dae1c6b36"), new DateTime(2023, 7, 17, 8, 51, 6, 900, DateTimeKind.Utc).AddTicks(5915), "france", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "coding", "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
