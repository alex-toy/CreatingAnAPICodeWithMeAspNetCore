using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class healthdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("a593056e-76d6-423f-b158-932aff0ee9d6"));

            migrationBuilder.CreateTable(
                name: "HealthData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseGlasses = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Address", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "Hobby", "IdentityId", "LastName", "MobileNumber", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("69dc5b78-944f-447a-8969-8e5ca7c9cf72"), new DateTime(2023, 7, 19, 11, 37, 20, 200, DateTimeKind.Utc).AddTicks(6544), null, "france", new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", null, "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", null, "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthData");

            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("69dc5b78-944f-447a-8969-8e5ca7c9cf72"));

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Address", "Country", "DateOfBirth", "Email", "FirstName", "Gender", "Hobby", "IdentityId", "LastName", "MobileNumber", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("a593056e-76d6-423f-b158-932aff0ee9d6"), new DateTime(2023, 7, 19, 5, 47, 4, 275, DateTimeKind.Utc).AddTicks(9437), null, "france", new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", null, "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", null, "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
