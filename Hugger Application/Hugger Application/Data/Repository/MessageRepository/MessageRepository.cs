using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.MessageRepository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly UserContext _context;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(UserContext context, ILogger<MessageRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Message> GetLastMessageByDateAsync(string date)
        {
            throw new NotImplementedException();
        }

        public async Task<Message[]> GetMessagesByMatchIdAsync(int matchId)
        {
            _logger.LogInformation($"Getting messeges for matchId= {matchId}");

            IQueryable<Message> messagesQuery = _context.Messages
                .Where(msg => msg.MatchId == matchId);

            return await messagesQuery.ToArrayAsync();

        }
    }
}
