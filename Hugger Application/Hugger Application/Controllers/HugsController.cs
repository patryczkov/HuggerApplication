﻿using AutoMapper;
using Hugger_Application.Data.Repository.HugRepository;
using Hugger_Application.Models;
using Hugger_Application.Models.MatchDTO;
using Hugger_Application.Models.Repository;
using Hugger_Application.Services.HugService;
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
    /// Controller of hugs of each user
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("/hugger/users/{userId}/[controller]")]
    public class HugsController : ControllerBase
    {
        private readonly IHugRepository _hugRepository;
        private readonly IHugService _hugService;
        private readonly IMapper _mapper;
        private readonly ILogger<HugsController> _logger;

        public HugsController(IHugRepository hugRepository, IHugService hugService, IMapper mapper, ILogger<HugsController> logger)
        {
            _hugRepository = hugRepository;
            _hugService = hugService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Get all hugs for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Id of hug and Id of sender and receiver</returns>
        /// <response code="200">Return hugs</response> 
        /// <response code="404">Hugs not found</response> 
        /// <response code="500">Server not responding</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HugDTO[]>> Get(int userId)
        {
            _logger.LogInformation($"GET hugs for userId= {userId}");
            try
            {
                var hugs = await _hugRepository.GetHugsBy_SenderUserIdAsync(userId);
                if (hugs.Length == 0 || hugs == null) return NotFound($"There is no hugs for this user");

                return Ok(_mapper.Map<HugDTO[]>(hugs));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");

            }
        }

        /// <summary>
        /// Get certain hug for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hugId"></param>
        /// <returns>Hug information of senderId and receiverId</returns>
        /// <response code="200">Return hug</response> 
        /// <response code="404">Hug or user not found</response> 
        /// <response code="500">Server not responding</response>
        [HttpGet("{hugId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HugDTO>> Get(int userId, int hugId)
        {
            _logger.LogInformation($"GET hugId= {hugId} for userId= {userId}");
            try
            {
                var hug = await _hugRepository.GetHugBy_SenderUserIdAndHugIdAsync(userId, hugId);
                if (hug == null) return NotFound($"There is no hug for this user");

                return Ok(_mapper.Map<HugDTO>(hug));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
        /// <summary>
        /// Create new hug for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hugDTO"></param>
        /// <returns></returns>
        /// <response code="201">Hug created</response>
        /// <response code="400">Hug could not be created</response> 
        /// <response code="404">User not found</response> 
        /// <response code="422">UserId is different from senderId</response>
        /// <response code="500">Server not responding</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HugDTO>> Post(int userId, HugDTO hugDTO)
        {
            _logger.LogInformation($"POST new hug");
            if (userId != hugDTO.UserIDSender) return UnprocessableEntity("UserId is different than model userId");
            try
            {
                var existingHug = await _hugRepository.GetHugsBy_SenderUserIdAsync(userId);

                if (existingHug.Where(h => h.UserIDSender == hugDTO.UserIDSender && h.UserIDReceiver == hugDTO.UserIDReceiver).Any())
                {
                    return BadRequest("Hug exists");
                }


                _logger.LogInformation($"Checking for match");
                await _hugService.CheckIfUsersHasAMatchAsync(hugDTO);


                //TODO HUG id autoincrement, context pool
                //context error - how to fix it?

                //var user = _userRepository.GetUserByIDAsync(userId);
                //if (user == null) return NotFound($"Userid= {userId} not exist");

                var hug = _mapper.Map<Hug>(hugDTO);
                _hugRepository.Create(hug);

                if (await _hugRepository.SaveChangesAsync()) return Created("", _mapper.Map<HugDTO>(hugDTO));
                else return BadRequest("Failed to add new hug");


            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
    }
}
