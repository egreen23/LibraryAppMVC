using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazionalita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<DateTime>(type: "date", nullable: false),
                    LuogoNascita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoD = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Cognome", "DoB", "DoD", "LuogoNascita", "Nazionalita", "Nome" },
                values: new object[,]
                {
                    { 1, "Maximo", new DateTime(2006, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Birmingham", "inglese", "Steve" },
                    { 2, "Terry", new DateTime(1942, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1984, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Londra", "inglese", "Harry" },
                    { 3, "Margiotta", new DateTime(1954, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birmingham", "italiana", "Mario" },
                    { 4, "Henry", new DateTime(1931, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1962, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birmingham", "francese", "Thierry" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
