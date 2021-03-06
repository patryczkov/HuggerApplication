﻿
using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.MatchRepository
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        private readonly UserContext _context;
        private readonly ILogger _logger;

        public MatchRepository(UserContext userContext, ILogger<MatchRepository> logger) : base(userContext)
        {
            _context = userContext;
            _logger = logger;
        }

        public async Task<Match[]> GetAllMatchesAsync()
        {
            _logger.LogInformation("Getting all matches");

            IQueryable<Match> matchesQuery = _context.Matches;

            return await matchesQuery.ToArrayAsync();


        }

        public async Task<Match[]> GetMatchesByUserIdAsync(int userId)
        {
            _logger.LogInformation($"Getting matches for userId= {userId}");
            
            IQueryable<Match> matchesQuery = _context.Matches;
            matchesQuery.Where(mtch => mtch.UserIDSender == userId || mtch.UserIDReceiver == userId);

            return await matchesQuery.ToArrayAsync();


        }


        public async Task<Match[]> GetMatchesByDateOfMatch(string date)
        {
            _logger.LogInformation($"Getting matches by date= {date}");

            IQueryable<Match> matchesQuery = _context.Matches;
            matchesQuery.Where(mtch => mtch.MatchDate == date);

            return await matchesQuery.ToArrayAsync();
        }

        public async Task<Match> GetMatchByUserId_UserReceiverIdAsync(int userId, int userReceiverId)
        {
            _logger.LogInformation($"Getting match for user pair= {userId} and {userReceiverId}");

            IQueryable<Match> matchQuery = _context.Matches;
            matchQuery.Where(mtch => mtch.UserIDSender == userId && mtch.UserIDReceiver == userReceiverId);

            return await matchQuery.FirstOrDefaultAsync();

        }
    }
}
