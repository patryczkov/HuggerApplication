using AutoMapper;
using Hugger_Application.Data.Repository.MessageRepository;
using Hugger_Application.Models;
using Hugger_Application.Models.MessageDTO;
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
    /// Controller for messages between users with match
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("hugger/{userId}/{matchId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessagesController> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for messages controller
        /// </summary>
        /// <param name="messageRepository"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public MessagesController(IMessageRepository messageRepository, ILogger<MessagesController> logger, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all messages for match
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns>Messages between matched users</returns>
        /// <response code="200">Return messages</response> 
        /// <response code="404">Messages not found</response> 
        /// <response code="500">Server not responding</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MessageGetDTO[]>> Get(int matchId)
        {
            _logger.LogInformation($"GET messages for matchId= {matchId}");
            try
            {
                var messages = await _messageRepository.GetMessagesByMatchIdAsync(matchId);
                if (messages.Length == 0 || messages == null) return NotFound($"There is no messages for this match");

                return Ok(_mapper.Map<MessageGetDTO[]>(messages));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");

            }
        }

        /// <summary>
        /// Create new message from userSender
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="messageCreateDTO"></param>
        /// <returns></returns>
        /// <response code="201">Message created</response>
        /// <response code="400">Message could not be created</response> 
        /// <response code="404">User not found</response> 
        /// <response code="422">UserId is different from senderId</response>
        /// <response code="500">Server not responding</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MessageGetDTO>> Post(int userId, MessageCreateDTO messageCreateDTO)
        {
            _logger.LogInformation($"POST new message from userID= {userId}");
            if (userId != messageCreateDTO.UserSenderId) return UnprocessableEntity("UserSenderId is different than model userId");
            try
            {
                var newMessage = _mapper.Map<Message>(messageCreateDTO);
                _messageRepository.Create(newMessage);

                if (await _messageRepository.SaveChangesAsync()) return Created("", _mapper.Map<MessageGetDTO>(messageCreateDTO));
                else return BadRequest("Failed to add new message");


            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }


        }



        //TODO delete messages older than 7 days

/*
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MessageGetDTO>> Delete(int matchId)
        {
            _logger.LogInformation($"DELETE message for matchID= {matchId}");
            try
            {
                var oldMessage = await _messageRepository.GetMessagesByMatchIdAsync(matchId);
                if (oldMessage == null) return NotFound($"Could not find a message");

                _messageRepository.Delete(oldMessage);
                if (await _messageRepository.SaveChangesAsync()) return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest("Failed to delete message");
        }
        */

    }
}
