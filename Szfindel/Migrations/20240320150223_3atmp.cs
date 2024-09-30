using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class _3atmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AccountUsers_AccountUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AccountUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountUserId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AccountUserId",
                table: "Messages",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AccountUsers_AccountUserId",
                table: "Messages",
                column: "AccountUserId",
                principalTable: "AccountUsers",
                principalColumn: "AccountUserId");
        }
    }
}
