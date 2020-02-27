using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.UserPrefRepository
{
    public class UserPrefRepository : Repository<UserPreference>, IUserPrefRepository
    {
        private readonly UserContext _context;
        private readonly ILogger<UserPrefRepository> _logger;

        public UserPrefRepository(UserContext context, ILogger<UserPrefRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<UserPreference[]> GetUsersPreferenceByNameAsync(string prefName)
        {
            _logger.LogInformation($"Getting preferance by name= {prefName}");

            var preferencesQuery = _context.Users_Preferences
                .Include(usrPref=> usrPref.Preference)
                .Where(usrPref => usrPref.Preference.Name == prefName);
            

            return await preferencesQuery.ToArrayAsync();
        }

        public async Task<UserPreference[]> GetUserPreferencesAsync(int userId)
        {
            _logger.LogInformation($"Getting preferances for userId= {userId}");


            var userPreferences = _context.Users_Preferences
                .Include(z => z.Preference)
            .Include(z => z.User)
            .Where(z => z.UserId == userId); 
            
            return await userPreferences.ToArrayAsync();


        }

        public async Task<UserPreference> GetUserPreferenceByName_UserIDAsync(string prefName, int userId)
        {
            _logger.LogInformation($"Getting preferance by name= {prefName} for userId= {userId}");

            var userPreferences = _context.Users_Preferences
               .Include(z => z.Preference)
           .Include(z => z.User)
           .Where(z => z.UserId == userId && z.Preference.Name == prefName );

            return await userPreferences.FirstOrDefaultAsync();

        }

        public async Task<Preference> GetPreferenceByNameAsync(string prefName)
        {
            _logger.LogInformation($"Getting preference= {prefName}");

            var preference = _context.Preferences
                .Where(pref => pref.Name == prefName);

            return await preference.FirstOrDefaultAsync();
        }
    }
}
