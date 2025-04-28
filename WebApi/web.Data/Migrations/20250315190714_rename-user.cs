using Microsoft.EntityFrameworkCore.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class renameuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreationList_ChallengeList_ChallengeId",
                table: "CreationList");

            migrationBuilder.DropForeignKey(
                name: "FK_CreationList_UserList_UserId",
                table: "CreationList");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_CreationList_CreationId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_WinCreations_ChallengeList_ChallengeId",
                table: "WinCreations");

            migrationBuilder.DropForeignKey(
                name: "FK_WinCreations_CreationList_CreationId",
                table: "WinCreations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserList",
                table: "UserList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreationList",
                table: "CreationList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeList",
                table: "ChallengeList");

            migrationBuilder.RenameTable(
                name: "UserList",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "CreationList",
                newName: "Creations");

            migrationBuilder.RenameTable(
                name: "ChallengeList",
                newName: "Challenges");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_CreationList_UserId",
                table: "Creations",
                newName: "IX_Creations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CreationList_ChallengeId",
                table: "Creations",
                newName: "IX_Creations_ChallengeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creations",
                table: "Creations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Challenges",
                table: "Challenges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Challenges_ChallengeId",
                table: "Creations",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Users_UserId",
                table: "Creations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Creations_CreationId",
                table: "Ratings",
                column: "CreationId",
                principalTable: "Creations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WinCreations_Challenges_ChallengeId",
                table: "WinCreations",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WinCreations_Creations_CreationId",
                table: "WinCreations",
                column: "CreationId",
                principalTable: "Creations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Challenges_ChallengeId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Users_UserId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Creations_CreationId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_WinCreations_Challenges_ChallengeId",
                table: "WinCreations");

            migrationBuilder.DropForeignKey(
                name: "FK_WinCreations_Creations_CreationId",
                table: "WinCreations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creations",
                table: "Creations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Challenges",
                table: "Challenges");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserList");

            migrationBuilder.RenameTable(
                name: "Creations",
                newName: "CreationList");

            migrationBuilder.RenameTable(
                name: "Challenges",
                newName: "ChallengeList");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "UserList",
                newName: "IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_UserId",
                table: "CreationList",
                newName: "IX_CreationList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_ChallengeId",
                table: "CreationList",
                newName: "IX_CreationList_ChallengeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserList",
                table: "UserList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreationList",
                table: "CreationList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeList",
                table: "ChallengeList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreationList_ChallengeList_ChallengeId",
                table: "CreationList",
                column: "ChallengeId",
                principalTable: "ChallengeList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreationList_UserList_UserId",
                table: "CreationList",
                column: "UserId",
                principalTable: "UserList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_CreationList_CreationId",
                table: "Ratings",
                column: "CreationId",
                principalTable: "CreationList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WinCreations_ChallengeList_ChallengeId",
                table: "WinCreations",
                column: "ChallengeId",
                principalTable: "ChallengeList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WinCreations_CreationList_CreationId",
                table: "WinCreations",
                column: "CreationId",
                principalTable: "CreationList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

















