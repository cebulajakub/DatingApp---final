using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class cObbies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Hobby_HobbyId",
                table: "UserHobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby");

            migrationBuilder.RenameTable(
                name: "Hobby",
                newName: "Hobbies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies",
                column: "HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Hobbies_HobbyId",
                table: "UserHobby",
                column: "HobbyId",
                principalTable: "Hobbies",
                principalColumn: "HobbyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Hobbies_HobbyId",
                table: "UserHobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies");

            migrationBuilder.RenameTable(
                name: "Hobbies",
                newName: "Hobby");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby",
                column: "HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Hobby_HobbyId",
                table: "UserHobby",
                column: "HobbyId",
                principalTable: "Hobby",
                principalColumn: "HobbyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
