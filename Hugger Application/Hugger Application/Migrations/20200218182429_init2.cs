using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hugs_Users_UserId",
                table: "Hugs");

            migrationBuilder.DropIndex(
                name: "IX_Hugs_UserId",
                table: "Hugs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hugs");

            migrationBuilder.CreateIndex(
                name: "IX_Hugs_UserIDSender",
                table: "Hugs",
                column: "UserIDSender");

            migrationBuilder.AddForeignKey(
                name: "FK_Hugs_Users_UserIDSender",
                table: "Hugs",
                column: "UserIDSender",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hugs_Users_UserIDSender",
                table: "Hugs");

            migrationBuilder.DropIndex(
                name: "IX_Hugs_UserIDSender",
                table: "Hugs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hugs_UserId",
                table: "Hugs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hugs_Users_UserId",
                table: "Hugs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
