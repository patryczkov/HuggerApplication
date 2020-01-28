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
            modelBuilder.Entity<UserCharacteristic>().HasKey(UserChar => new { UserChar.UserId, UserChar.CharacteristicId });

            modelBuilder.Entity<UserCharacteristic>()
                .HasOne<User>(UserChar => UserChar.User)
                .WithMany(Usr => Usr.UserCharacteristics)
                .HasForeignKey(UserChar => UserChar.UserId);

            modelBuilder.Entity<UserCharacteristic>()
                .HasOne<Characteristic>(UserChar => UserChar.Characteristic)
                .WithMany(Char => Char.UserCharacteristics)
                .HasForeignKey(UserChar => UserChar.CharacteristicId);


            








        }

    }
}
