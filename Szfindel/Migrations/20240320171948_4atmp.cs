using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class _4atmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccountUsers_AccountUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccountUserId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AccountUserId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AccountUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountUserId",
                table: "Users",
                column: "AccountUserId",
                unique: true,
                filter: "[AccountUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccountUsers_AccountUserId",
                table: "Users",
                column: "AccountUserId",
                principalTable: "AccountUsers",
                principalColumn: "AccountUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccountUsers_AccountUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccountUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AccountUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AccountUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountUserId",
                table: "Users",
                column: "AccountUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccountUsers_AccountUserId",
                table: "Users",
                column: "AccountUserId",
                principalTable: "AccountUsers",
                principalColumn: "AccountUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
