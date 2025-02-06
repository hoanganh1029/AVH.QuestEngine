using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVH.QuestEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlayerQuestIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerQuestTurns_PlayerId_QuestId",
                table: "PlayerQuestTurns",
                columns: new[] { "PlayerId", "QuestId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerQuestTurns_PlayerId_QuestId",
                table: "PlayerQuestTurns");
        }
    }
}
