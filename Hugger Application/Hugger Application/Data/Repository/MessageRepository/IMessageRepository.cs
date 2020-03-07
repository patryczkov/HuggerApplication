using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.MessageRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message[]> GetMessagesByMatchIdAsync(int matchId);
        Task<Message> GetLastMessageByDateAsync(string date);
    }
}
