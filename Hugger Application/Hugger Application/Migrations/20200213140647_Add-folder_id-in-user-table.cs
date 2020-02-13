using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class Addfolder_idinusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderId",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "User");
        }
    }
}
