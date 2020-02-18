using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.HugRepository
{
    public class HugRepository : Repository<Hug>, IHugRepository
    {
        private readonly UserContext _context;
        private readonly ILogger _logger;

        public HugRepository(UserContext context, ILogger<HugRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Hug[]> GetHugsBy_SenderUserIdAsync(int userId)
        {
            _logger.LogInformation($"Getting all hugs for userId= {userId}");

            IQueryable<Hug> hugsQuery = _context.Hugs;

            hugsQuery = hugsQuery
                .Where(h => h.UserIDSender == userId)
                .OrderBy(h => h.UserIDReceiver);

            return await hugsQuery.ToArrayAsync();
        }


        public async Task<Hug> GetHugBy_SenderUserIdAndHugIdAsync(int userId, int hugId)
        {
            _logger.LogInformation($"Getting hub by hugId= {hugId} and userId= {userId}");

            IQueryable<Hug> hugQuery = _context.Hugs;

            hugQuery = hugQuery
                .Where(h => h.Id == hugId && h.UserIDSender == userId);

            return await hugQuery.FirstOrDefaultAsync();

        }
        
    }
}
