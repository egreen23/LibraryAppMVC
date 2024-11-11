using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    DataPubblicazione = table.Column<DateTime>(type: "date", nullable: false),
                    Genere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "DataPubblicazione", "Genere", "Prezzo", "Quantita", "Titolo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1999, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasia", 1.30m, 3, "Biancaneve" },
                    { 2, 1, new DateTime(1988, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasia", 1.80m, 3, "Cenerentola" },
                    { 3, 2, new DateTime(2004, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller", 3m, 1, "Oxford Murderers" },
                    { 4, 3, new DateTime(1921, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romanzo", 2.50m, 3, "Orgoglio e Pregiudizio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
