﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Hugger_Application.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPS = table.Column<string>(nullable: true),
                    LocalizationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FolderPath = table.Column<string>(nullable: true),
                    LastWatchedUserId = table.Column<int>(nullable: false),
                    LocalizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_User_Localizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "Localizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hugs",
                columns: table => new
                {
                    UserUUIDSender = table.Column<int>(nullable: false),
                    UserUUIDReceiver = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hugs", x => new { x.UserUUIDSender, x.UserUUIDReceiver });
                    table.ForeignKey(
                        name: "FK_Hugs_User_UserUUIDReceiver",
                        column: x => x.UserUUIDReceiver,
                        principalTable: "User",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCharacteristic",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CharacteristicId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCharacteristic", x => new { x.UserId, x.CharacteristicId });
                    table.ForeignKey(
                        name: "FK_UserCharacteristic_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCharacteristic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Preferences",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    PreferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Preferences", x => new { x.UserId, x.PreferenceId });
                    table.ForeignKey(
                        name: "FK_Users_Preferences_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Preferences_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<string>(nullable: true),
                    HugID = table.Column<int>(nullable: false),
                    HugUserUUIDSender = table.Column<int>(nullable: false),
                    HugUserUUIDReceiver = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Hugs_HugUserUUIDSender_HugUserUUIDReceiver",
                        columns: x => new { x.HugUserUUIDSender, x.HugUserUUIDReceiver },
                        principalTable: "Hugs",
                        principalColumns: new[] { "UserUUIDSender", "UserUUIDReceiver" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfSend = table.Column<string>(nullable: true),
                    WasRead = table.Column<bool>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hugs_UserUUIDReceiver",
                table: "Hugs",
                column: "UserUUIDReceiver");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HugUserUUIDSender_HugUserUUIDReceiver",
                table: "Matches",
                columns: new[] { "HugUserUUIDSender", "HugUserUUIDReceiver" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MatchId",
                table: "Messages",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LocalizationId",
                table: "User",
                column: "LocalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCharacteristic_CharacteristicId",
                table: "UserCharacteristic",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Preferences_PreferenceId",
                table: "Users_Preferences",
                column: "PreferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserCharacteristic");

            migrationBuilder.DropTable(
                name: "Users_Preferences");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Hugs");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Localizations");
        }
    }
}