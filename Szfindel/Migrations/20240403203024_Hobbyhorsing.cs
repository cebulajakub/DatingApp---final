using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class Hobbyhorsing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "HobbyId", "HobbyName" },
                values: new object[,]
                {
                    { 4, "ONS" },
                    { 5, "FWB" },
                    { 6, "Muzyka" },
                    { 7, "Gotowanie" },
                    { 8, "Wędkowanie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 8);
        }
    }
}
