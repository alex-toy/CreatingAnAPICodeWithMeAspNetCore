using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SohatNotebook.Api.Migrations
{
    /// <inheritdoc />
    public partial class refresh_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("a4d897e2-d6e8-444d-8c68-0e2fd9c3e749"));

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "Hobby", "IdentityId", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("089425fa-0ba1-40c5-8466-df892de53325"), new DateTime(2023, 7, 18, 7, 31, 51, 225, DateTimeKind.Utc).AddTicks(1392), "france", new DateTime(2023, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: new Guid("089425fa-0ba1-40c5-8466-df892de53325"));

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "AddedData", "Country", "DateOfBirth", "Email", "FirstName", "Hobby", "IdentityId", "LastName", "Phone", "Profession", "Status", "UpdateDate" },
                values: new object[] { new Guid("a4d897e2-d6e8-444d-8c68-0e2fd9c3e749"), new DateTime(2023, 7, 17, 8, 56, 2, 596, DateTimeKind.Utc).AddTicks(5247), "france", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), "alex@test.fr", "alex", "coding", new Guid("00000000-0000-0000-0000-000000000000"), "rea", "1234", "developper", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
