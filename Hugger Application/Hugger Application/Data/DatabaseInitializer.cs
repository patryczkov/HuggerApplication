using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(ApplicationContext applicationContext)
        {
            applicationContext.Database.EnsureCreated();

            if (applicationContext.Users.Any())
            {
                return;
            }
            var users = new User[]
            {
                new User{Id = 1, UUID= 555444666, Login="pati", Password="pati", LocalizationId= 1, FolderPath="test/pati",LastWatchedUserId=2},
                new User{Id = 2, UUID= 45454545, Login="hubi", Password="hubi", LocalizationId= 1, FolderPath="test/hubi",LastWatchedUserId=1}
            };

            foreach (var user in users)
            {
                applicationContext.Users.Add(user);
            }
        }
    }
}
