﻿// <auto-generated />
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hugger_Application.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20200224124528_Fix-of-relationship-between-matches-and-users")]
    partial class Fixofrelationshipbetweenmatchesandusers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hugger_Web_Application.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Hug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserIDReceiver")
                        .HasColumnType("int");

                    b.Property<int>("UserIDSender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIDSender");

                    b.ToTable("Hugs");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Localization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GPS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalizationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MatchDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserIDReceiver")
                        .HasColumnType("int");

                    b.Property<int>("UserIDSender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIDSender");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<string>("TimeOfSend")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WasRead")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Preference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BirthYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FolderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FolderPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LastWatchedUserId")
                        .HasColumnType("int");

                    b.Property<int>("LocalizationId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocalizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.UserCharacteristic", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "CharacteristicId");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("Users_Characteristics");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.UserPreference", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PreferenceId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "PreferenceId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("Users_Preferences");
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Hug", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIDSender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Match", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.User", "User")
                        .WithMany("Matches")
                        .HasForeignKey("UserIDSender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.Message", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.Match", "Match")
                        .WithMany("Messages")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.User", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.Localization", "Localization")
                        .WithMany()
                        .HasForeignKey("LocalizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.UserCharacteristic", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.Characteristic", "Characteristic")
                        .WithMany("UserCharacteristics")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hugger_Web_Application.Models.User", "User")
                        .WithMany("UserCharacteristics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hugger_Web_Application.Models.UserPreference", b =>
                {
                    b.HasOne("Hugger_Web_Application.Models.Preference", "Preference")
                        .WithMany("UsersPreferences")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hugger_Web_Application.Models.User", "User")
                        .WithMany("UsersPreferences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
