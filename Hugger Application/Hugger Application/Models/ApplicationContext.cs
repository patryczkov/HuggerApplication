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
        public DbSet<User> Users { get; set; }
        public DbSet<User_Characteristic> Users_Characteristics { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<User_Preference> Users_Preferences { get; set; }
        public DbSet<Hug> Hugs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
