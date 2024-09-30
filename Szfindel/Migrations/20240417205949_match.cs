using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szfindel.Migrations
{
    /// <inheritdoc />
    public partial class match : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    MatchUserId = table.Column<int>(type: "int", nullable: false),
                    IsMatch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_AccountUsers_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUsers",
                        principalColumn: "AccountUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AccountUserId",
                table: "Matches",
                column: "AccountUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
