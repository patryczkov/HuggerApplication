using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.UserCharRepository
{
    public interface IUserCharRepository : IRepository<UserCharacteristic>
    {
        Task<UserCharacteristic[]> GetUserCharacteristicsByUserIdAsync(int userId);
        Task<UserCharacteristic> GetUserCharacteristicByCharID_UserIDAsync(int charId, int userId);
        Task<Characteristic> GetCharacteristicByNameAsync(string charName);
        Task<Characteristic> GetCharacteristicByCharIdAsync(int charId);

    }
}
