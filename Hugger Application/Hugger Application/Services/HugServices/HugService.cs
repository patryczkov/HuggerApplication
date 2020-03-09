using AutoMapper;
using Hugger_Application.Data.Repository.HugRepository;
using Hugger_Application.Models;
using Hugger_Application.Models.MatchDTO;
using Hugger_Web_Application.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services.HugService
{
    public class HugService : IHugService
    {
        private readonly IHugRepository _hugRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HugService> _logger;

        public HugService(IHugRepository hugRepository, IMapper mapper, ILogger<HugService> logger)
        {
            _hugRepository = hugRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CheckIfUsersHasAMatchAsync(HugDTO hugDTO)
        {
            _logger.LogInformation($"Looking for match of senderIg= {hugDTO.UserIDSender} ");
            var matchHug = await _hugRepository.GetHugBy_ReceiverId_UserSenderIdAsync(hugDTO.UserIDSender, hugDTO.UserIDReceiver);
            if (matchHug != null)
            {
                
                var createMatch = new MatchCreateDTO
                {
                    MatchDate = DateTime.UtcNow.ToString(),
                    UserIDSender = hugDTO.UserIDSender,
                    UserIDReceiver = hugDTO.UserIDReceiver
                };
                _logger.LogInformation("Found a match!");

                var match = _mapper.Map<Match>(createMatch);
                _hugRepository.CreateMatch(match);

            }
        }
    }
}
