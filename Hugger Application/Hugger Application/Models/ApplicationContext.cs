using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options ) : base(options)
        {

        }
        public DbSet<UserCharacteristic> Users { get; set; }
        public DbSet<UserCharacteristic> Users_Characteristics { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<UserPreference> Users_Preferences { get; set; }
        public DbSet<Hug> Hugs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //join table User - Characteristic
            modelBuilder.Entity<UserCharacteristic>().HasKey(UserChar => new { UserChar.UserId, UserChar.CharacteristicId });

            modelBuilder.Entity<UserCharacteristic>()
                .HasOne<User>(UserChar => UserChar.User)
                .WithMany(Usr => Usr.UserCharacteristics)
                .HasForeignKey(UserChar => UserChar.UserId);

            modelBuilder.Entity<UserCharacteristic>()
                .HasOne<Characteristic>(UserChar => UserChar.Characteristic)
                .WithMany(Char => Char.UserCharacteristics)
                .HasForeignKey(UserChar => UserChar.CharacteristicId);

            //join table User - Preference
            modelBuilder.Entity<UserPreference>().HasKey(UserPref => new { UserPref.UserId, UserPref.PreferenceId });

            modelBuilder.Entity<UserPreference>()
                .HasOne<User>(UserPref => UserPref.User)
                .WithMany(Usr => Usr.UsersPreferences)
                .HasForeignKey(UserPref => UserPref.UserId);

            modelBuilder.Entity<UserPreference>()
                .HasOne<Preference>(UserPref => UserPref.Preference)
                .WithMany(Pref => Pref.UsersPreferences)
                .HasForeignKey(UserPref => UserPref.PreferenceId);

            //join table User - Match (Hugs)
            modelBuilder.Entity<Hug>().HasKey(Hg => new { Hg.UserUUIDSender, Hg.UserUUIDReceiver });

            modelBuilder.Entity<Hug>()
                .HasOne<User>(Hg => Hg.User)
                .WithMany(Usr => Usr.Hugs)
                .HasForeignKey(Hg => Hg.UserUUIDSender);

            modelBuilder.Entity<Hug>()
                .HasOne<User>(Hg => Hg.User)
                .WithMany(Usr => Usr.Hugs)
                .HasForeignKey(Hg => Hg.UserUUIDReceiver);











        }

    }
}
