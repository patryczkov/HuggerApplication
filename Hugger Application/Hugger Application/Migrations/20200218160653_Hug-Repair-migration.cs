using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class HugRepairmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hugs_User_UserIDReceiver",
                table: "Hugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_User_UserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Localizations_LocalizationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characteristics_User_UserId",
                table: "Users_Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Preferences_User_UserId",
                table: "Users_Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_LocalizationId",
                table: "Users",
                newName: "IX_Users_LocalizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hugs_Users_UserIDReceiver",
                table: "Hugs",
                column: "UserIDReceiver",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Localizations_LocalizationId",
                table: "Users",
                column: "LocalizationId",
                principalTable: "Localizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characteristics_Users_UserId",
                table: "Users_Characteristics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Preferences_Users_UserId",
                table: "Users_Preferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hugs_Users_UserIDReceiver",
                table: "Hugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Localizations_LocalizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characteristics_Users_UserId",
                table: "Users_Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Preferences_Users_UserId",
                table: "Users_Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LocalizationId",
                table: "User",
                newName: "IX_User_LocalizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hugs_User_UserIDReceiver",
                table: "Hugs",
                column: "UserIDReceiver",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_User_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Localizations_LocalizationId",
                table: "User",
                column: "LocalizationId",
                principalTable: "Localizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characteristics_User_UserId",
                table: "Users_Characteristics",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Preferences_User_UserId",
                table: "Users_Preferences",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
