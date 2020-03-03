using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.UserCharRepository
{
    public class UserCharRepository : Repository<UserCharacteristic>, IUserCharRepository
    {
        private readonly UserContext _context;
        private readonly ILogger<UserCharRepository> _logger;
        private object pref;

        public UserCharRepository(UserContext context, ILogger<UserCharRepository> logger): base(context)
        {
            _context = context;
            _logger = logger;
        }





        public async Task<Characteristic> GetCharacteristicByNameAsync(string charName)
        {
            _logger.LogInformation($"Getting characteristic by name= {charName}");

            var characteristic = _context.Characteristics
                .Where(chr => chr.Name == charName);

            return await characteristic.FirstOrDefaultAsync();
        }

        public async Task<Characteristic> GetCharacteristicByCharIdAsync(int charId)
        {
            _logger.LogInformation($"Getting characteristic by id= {charId}");

            var characteristic = _context.Characteristics
                .Where(chr => chr.Id == charId);

            return await characteristic.FirstOrDefaultAsync();
        }

        public async Task<UserCharacteristic> GetUserCharacteristicByCharID_UserIDAsync(int charId, int userId)
        {
            _logger.LogInformation($"Getting characteristics for userId= {userId} and charId= {charId}");

            var userPreferences = _context.Users_Characteristics
                .Include(usrchr => usrchr.Characteristic)
                .Include(usrchr => usrchr.User)
                .Where(usrchr => usrchr.UserId == userId);

            return await userPreferences.FirstOrDefaultAsync();
        }

        public async Task<UserCharacteristic[]> GetUserCharacteristicsByUserIdAsync(int userId)
        {
            _logger.LogInformation($"Getting characteristics for userId= {userId}");

            var userPreferences = _context.Users_Characteristics
                .Include(usrchr => usrchr.Characteristic)
                .Include(usrchr => usrchr.User)
                .Where(usrchr => usrchr.UserId == userId);

            return await userPreferences.ToArrayAsync();
        }
    }
}
