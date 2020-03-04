using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hugger_Application.Migrations
{
    public partial class Add_server_role_table_and_user_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerRoleId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServerRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServerRoleId",
                table: "Users",
                column: "ServerRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ServerRoles_ServerRoleId",
                table: "Users",
                column: "ServerRoleId",
                principalTable: "ServerRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ServerRoles_ServerRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ServerRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_ServerRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ServerRoleId",
                table: "Users");
        }
    }
}
