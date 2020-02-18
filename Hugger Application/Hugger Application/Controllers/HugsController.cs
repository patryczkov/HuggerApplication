using AutoMapper;
using Hugger_Application.Data.Repository.HugsRepository;
using Hugger_Application.Models;
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
    /// Controller of hugs of each user
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/hugger/users/{userId}/hugs")]
    public class HugsController : ControllerBase
    {
        private readonly IHugRepository _hugRepository;
        private readonly IMapper _mapper;

        public HugsController(IHugRepository hugRepository, IMapper mapper)
        {
            _hugRepository = hugRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Getting all hugs for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Id of hug and Id of sender and receiver</returns>
        /// <response code="200">Return hugs</response> 
        /// <response code="404">Hugs not found</response> 
        /// <response code="500">Server not responding</response>

        [HttpGet("userId:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HugDTO[]>> Get(int userId)
        {
            var hugs = await _hugRepository.GetHugsBy_SenderUserIdAsync(userId);
            if (hugs == null) return NotFound($"There is no hugs fo userId= {userId}");

            return Ok(_mapper.Map<HugDTO[]>(hugs));
        }
    }
}
