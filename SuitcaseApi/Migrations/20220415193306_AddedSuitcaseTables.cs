using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuitcaseApi.Migrations
{
    public partial class AddedSuitcaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Country_pk", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsPacked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Item_pk", x => x.IdItem);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: true),
                    TokenExpire = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_Id", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("City_pk", x => x.IdCity);
                    table.ForeignKey(
                        name: "City_Country",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suitcase",
                columns: table => new
                {
                    IdSuitcase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCity = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Suitcase_pk", x => x.IdSuitcase);
                    table.ForeignKey(
                        name: "Suitcase_City",
                        column: x => x.IdCity,
                        principalTable: "City",
                        principalColumn: "IdCity",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Suitcase_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suitcase_Item",
                columns: table => new
                {
                    IdSuitcase = table.Column<int>(type: "int", nullable: false),
                    IdItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SuitcaseItem_pk", x => new { x.IdSuitcase, x.IdItem });
                    table.ForeignKey(
                        name: "Item_SuitcaseItem",
                        column: x => x.IdItem,
                        principalTable: "Item",
                        principalColumn: "IdItem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Suitcase_SuitcaseItem",
                        column: x => x.IdSuitcase,
                        principalTable: "Suitcase",
                        principalColumn: "IdSuitcase",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "IdUser", "Email", "HashedPassword", "Login", "RefreshToken", "TokenExpire" },
                values: new object[] { 1, "a@gmail.com", "AQAAAAEAACcQAAAAEDJk1ku5Q8iQTFoC0dQFUCQ8W8YQklfB4DKyDLb/25cFsmGQiFcBd+9E6w6OpViCRA==", "Adam", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_City_IdCountry",
                table: "City",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcase_IdCity",
                table: "Suitcase",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcase_IdUser",
                table: "Suitcase",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcase_Item_IdItem",
                table: "Suitcase_Item",
                column: "IdItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suitcase_Item");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Suitcase");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
