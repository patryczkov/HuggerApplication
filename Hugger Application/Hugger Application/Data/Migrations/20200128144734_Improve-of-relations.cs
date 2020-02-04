using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class Improveofrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HugID",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "HugUserUUIDSender",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HugUserUUIDReceiver",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserUUID",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserUUIDReceiver",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserUUIDSender",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserUUID",
                table: "Matches",
                column: "UserUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_User_UserUUID",
                table: "Matches",
                column: "UserUUID",
                principalTable: "User",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches",
                columns: new[] { "HugUserUUIDSender", "HugUserUUIDReceiver" },
                principalTable: "Hugs",
                principalColumns: new[] { "UserUUIDSender", "UserUUIDReceiver" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_User_UserUUID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserUUID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserUUID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserUUIDSender",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "HugUserUUIDSender",
                table: "Matches",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HugUserUUIDReceiver",
                table: "Matches",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HugID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches",
                columns: new[] { "HugUserUUIDSender", "HugUserUUIDReceiver" },
                principalTable: "Hugs",
                principalColumns: new[] { "UserUUIDSender", "UserUUIDReceiver" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
