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

        public UserPrefRepository(UserContext context, ILogger<UserPrefRepository> logger): base(context)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<Preference[]> GetAllPreferancesAsync()
        {
            _logger.LogInformation("Getting all preferances from database");

            IQueryable<Preference> preferencesQuery = _context.Preferences;

            return await preferencesQuery.ToArrayAsync();

        }

        public async Task<Preference> GetPreferenceByNameAsync(string prefName)
        {
            _logger.LogInformation($"Getting preferance by name= {prefName}");

            IQueryable<Preference> preferencesQuery = _context.Preferences;
            preferencesQuery.Select(pref => pref.Name == prefName);

            return await preferencesQuery.FirstOrDefaultAsync();
        }

        public async Task<UserPreference[]> GetUserPreferencesAsync(int userId)
        {
            _logger.LogInformation($"Getting preferances for userId= {userId}");

            IQueryable<UserPreference> userPreferencesQuery = _context.Users_Preferences;

            userPreferencesQuery.Where(x => x.UserId == userId)
                .Select(x => x.Preference.Name);


            return await userPreferencesQuery.ToArrayAsync();


            
        }

        public Task<UserPreference[]> GetAllPreferancesWithUserId()
        {
            throw new NotImplementedException();
        }

    }
}
