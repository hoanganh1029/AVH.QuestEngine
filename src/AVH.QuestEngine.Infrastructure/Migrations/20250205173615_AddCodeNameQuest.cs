using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVH.QuestEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeNameQuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Quests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Quests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Quests");
        }
    }
}
