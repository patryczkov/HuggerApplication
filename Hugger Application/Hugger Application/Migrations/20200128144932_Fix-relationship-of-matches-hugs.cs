using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class Fixrelationshipofmatcheshugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HugUserUUIDReceiver",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HugUserUUIDSender",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HugUserUUIDReceiver",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HugUserUUIDSender",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches",
                columns: new[] { "HugUserUUIDSender", "HugUserUUIDReceiver" });

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches",
                columns: new[] { "HugUserUUIDSender", "HugUserUUIDReceiver" },
                principalTable: "Hugs",
                principalColumns: new[] { "UserUUIDSender", "UserUUIDReceiver" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
