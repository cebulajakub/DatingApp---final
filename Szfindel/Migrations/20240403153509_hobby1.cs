using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class hobby1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "HobbyName",
                value: "Netflix & CHill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "HobbyName",
                value: "Netflix & Hill");
        }
    }
}
