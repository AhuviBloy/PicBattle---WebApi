using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeuserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Creations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Challenges");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ChallengeId",
                table: "Ratings",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Challenges_ChallengeId",
                table: "Ratings",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Challenges_ChallengeId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ChallengeId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Creations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Challenges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
