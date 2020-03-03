using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hugger_Application.Migrations
{
    public partial class Fix_of_key_creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Preferences",
                table: "Users_Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Users_Preferences_PreferenceId",
                table: "Users_Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Characteristics",
                table: "Users_Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Users_Characteristics_CharacteristicId",
                table: "Users_Characteristics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users_Preferences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users_Characteristics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Preferences",
                table: "Users_Preferences",
                columns: new[] { "PreferenceId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Characteristics",
                table: "Users_Characteristics",
                columns: new[] { "CharacteristicId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Preferences",
                table: "Users_Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Characteristics",
                table: "Users_Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users_Preferences",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users_Characteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Preferences",
                table: "Users_Preferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Characteristics",
                table: "Users_Characteristics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Preferences_PreferenceId",
                table: "Users_Preferences",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Characteristics_CharacteristicId",
                table: "Users_Characteristics",
                column: "CharacteristicId");
        }
    }
}
