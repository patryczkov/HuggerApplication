using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.HugRepository
{
    public interface IHugRepository : IRepository<Hug>
    {
        Task<Hug[]> GetHugsBy_SenderUserIdAsync(int userId);
        Task<Hug> GetHugBy_SenderUserIdAndHugIdAsync(int userId, int hugId);
        Task<Hug> GetHugBy_ReceiverId_UserSenderIdAsync(int senderId, int receiverID);
        void CreateMatch(Match match);
    }
}
