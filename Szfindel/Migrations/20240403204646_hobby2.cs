using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class hobby2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "HobbyName",
                value: "Netflix & Hill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "HobbyName",
                value: "Netflix & CHill");
        }
    }
}
