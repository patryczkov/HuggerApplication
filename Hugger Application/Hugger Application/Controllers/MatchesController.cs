using AutoMapper;
using Hugger_Application.Data.Repository.MatchRepository;
using Hugger_Application.Models.MatchDTO;
using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public MatchesController(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchesRepository = matchRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all matches for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return all matches for user</returns>
        /// <response code="200">Return matches</response> 
        /// <response code="404">Match could not be found</response>
        /// <response code="500">If server not responding</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatchGetDTO[]>> Get(int userId)
        {
            try
            {
                var matchesForUser = await _matchesRepository.GetMatchesByUserIdAsync(userId);
                if (matchesForUser == null || matchesForUser.Length == 0) return NotFound($"Could not found  matches for userId= {userId}");

                return Ok(_mapper.Map<MatchGetDTO[]>(matchesForUser));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
        /// <summary>
        /// Remove match for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userReceiverId"></param>
        /// <returns>Remove match for user</returns>


        [HttpDelete("{userReceiverId:int}")]
        public async Task<ActionResult<MatchGetDTO>> Delete(int userId, int userReceiverId)
        {
            try
            {
                var oldMatch = await _matchesRepository.GetMatchByUserId_UserReceiverIdAsync(userId, userReceiverId);
                if (oldMatch == null) return NotFound($"Could not found  matches for userId= {userId} and receiverId= {userReceiverId}");

                _matchesRepository.Delete(oldMatch);

                if (await _matchesRepository.SaveChangesAsync()) return NoContent();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest("Failed to delete match");
        }




    }

}
