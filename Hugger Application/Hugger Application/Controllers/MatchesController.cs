using AutoMapper;
using Hugger_Application.Data.Repository.MatchRepository;
using Hugger_Application.Models.MatchDTO;
using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Controllers
{
    /// <summary>
    /// Controller of matches for each user
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("hugger/users/{userId}/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchRepository _matchesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MatchesController> _logger;

        public MatchesController(IMatchRepository matchRepository, IMapper mapper, ILogger<MatchesController> logger)
        {
            _matchesRepository = matchRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all matches for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return all matches for user</returns>
        /// <response code="200">Return matches</response> 
        /// <response code="404">Match could not be found</response>
        /// <response code="500">If server not responding</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatchGetDTO[]>> Get(int userId)
        {
            try
            {
                _logger.LogInformation($"GET matches for userId= {userId}");
                var matchesForUser = await _matchesRepository.GetMatchesByUserIdAsync(userId);
                if (matchesForUser == null || matchesForUser.Length == 0)
                {
                    _logger.LogInformation($"Matches not found for {userId}");
                    return NotFound($"Could not found  matches for userId= {userId}");
                }
                _logger.LogInformation($"Matches found for userId= {userId}.");
                return Ok(_mapper.Map<MatchGetDTO[]>(matchesForUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
        /// <summary>
        /// Remove match for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userReceiverId"></param>
        /// <returns>Remove match for user</returns>
        ///<response code="200">Return matches</response> 
        /// <response code="400">Match could not be delete</response>
        /// <response code="404">Match could not be found</response>
        /// <response code="500">If server not responding</response>



        [HttpDelete("{userReceiverId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatchGetDTO>> Delete(int userId, int userReceiverId)
        {
            try
            {
                _logger.LogInformation($"DELETE match for userId= {userId} and userReceiverId= {userReceiverId}");
                var oldMatch = await _matchesRepository.GetMatchByUserId_UserReceiverIdAsync(userId, userReceiverId);
                if (oldMatch == null)
                {
                    _logger.LogInformation($"Match not found for userId= {userId} and receiverid= {userReceiverId}");
                    return NotFound($"Could not found  matches for userId= {userId} and receiverId= {userReceiverId}");
                }


                _matchesRepository.Delete(oldMatch);

                if (await _matchesRepository.SaveChangesAsync())
                {
                    _logger.LogInformation($"Match deleted");
                    return NoContent(); 
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            _logger.LogInformation("Match delete failed");
            return BadRequest("Failed to delete match");
        }




    }

}
