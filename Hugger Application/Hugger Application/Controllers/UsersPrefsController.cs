using AutoMapper;
using Hugger_Application.Data.Repository.UserPrefRepository;
using Hugger_Application.Models.UserPreferancesDTO;
using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Controllers
{
    /// <summary>
    /// Controller for user-preferances
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("hugger/[controller]")]
    public class UsersPrefsController : Controller
    {
        private readonly IUserPrefRepository _userPrefRepository;
        private readonly ILogger<UsersPrefsController> _logger;
        private readonly IMapper _mapper;

        public UsersPrefsController(IUserPrefRepository userPrefRepository, ILogger<UsersPrefsController> logger, IMapper mapper)
        {
            _userPrefRepository = userPrefRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserPrefGetDTO[]>> Get(int userId)
        {
            _logger.LogInformation($"GET all preferances for usserId= {userId}");
            var userPrefs  = await _userPrefRepository.GetUserPreferencesAsync(userId);
            return Ok(_mapper.Map<UserPrefGetDTO[]>(userPrefs));

        }
        
    }
}
