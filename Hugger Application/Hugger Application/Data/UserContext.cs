using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hugger_Web_Application.Models;
using Microsoft.Extensions.Configuration;
using Hugger_Application.Data;
using Hugger_Application.Data.Entities;

namespace Hugger_Web_Application.Models
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            //contextOptionsBuilder.UseSqlServer(_configuration.GetConnectionString("HuggerContext"));
            contextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresContext"));

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCharacteristic> Users_Characteristics { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<ServerRole> ServerRoles { get; set; }
        public DbSet<Localization> Localizations { get; set; }
       
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<UserPreference> Users_Preferences { get; set; }
        public DbSet<Hug> Hugs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //join table User - Characteristic
            modelBuilder.Entity<UserCharacteristic>().HasKey(UserChar => new {UserChar.CharacteristicId, UserChar.UserId});
            /*
            modelBuilder.Entity<UserCharacteristic>()
                .HasOne(usrChar => usrChar.User)
                .WithMany(usr => usr.UserCharacteristics)
                .HasForeignKey(usrPref => usrPref.UserId);

            modelBuilder.Entity<UserCharacteristic>()
                .HasOne(usrChar => usrChar.Characteristic)
                .WithMany(pref => pref.UserCharacteristics)
                .HasForeignKey(usrPref => usrPref.CharacteristicId);
            //join table User - Preference
            */
           modelBuilder.Entity<UserPreference>().HasKey(UserPref => new { UserPref.PreferenceId, UserPref.UserId });
            /*
            modelBuilder.Entity<UserPreference>()
                .HasOne(usrPref => usrPref.User)
                .WithMany(usr => usr.UsersPreferences)
                .HasForeignKey(usrPref => usrPref.UserId);

            modelBuilder.Entity<UserPreference>()
                .HasOne(usrPref => usrPref.Preference)
                .WithMany(pref => pref.UsersPreferences)
                .HasForeignKey(usrPref => usrPref.PreferenceId);
                
    */
        }

    }
}

