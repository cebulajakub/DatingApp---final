using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class addHobby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AccountUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Hobby",
                columns: new[] { "HobbyId", "HobbyName" },
                values: new object[,]
                {
                    { 1, "Koszykówka" },
                    { 2, "Netflix & Hill" },
                    { 3, "Programming" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hobby",
                keyColumn: "HobbyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hobby",
                keyColumn: "HobbyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hobby",
                keyColumn: "HobbyId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AccountUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
