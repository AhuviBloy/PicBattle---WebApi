using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWinningCreationANdRaiting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    RateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_CreationList_CreationId",
                        column: x => x.CreationId,
                        principalTable: "CreationList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // שינה מ-CASCADE ל-NO ACTION
                });

            migrationBuilder.CreateTable(
                name: "WinCreations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    CreationId = table.Column<int>(type: "int", nullable: false),
                    WonAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinCreations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WinCreations_ChallengeList_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "ChallengeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // שינה מ-CASCADE ל-NO ACTION
                    table.ForeignKey(
                        name: "FK_WinCreations_CreationList_CreationId",
                        column: x => x.CreationId,
                        principalTable: "CreationList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // שינה מ-CASCADE ל-NO ACTION
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CreationId",
                table: "Ratings",
                column: "CreationId");

            migrationBuilder.CreateIndex(
                name: "IX_WinCreations_ChallengeId",
                table: "WinCreations",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_WinCreations_CreationId",
                table: "WinCreations",
                column: "CreationId");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "WinCreations");
        }
    }
}
