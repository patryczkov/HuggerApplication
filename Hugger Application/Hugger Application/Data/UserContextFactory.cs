using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data
{
    public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return new UserContext(new DbContextOptionsBuilder<UserContext>().Options, config);
        }
    }
}
