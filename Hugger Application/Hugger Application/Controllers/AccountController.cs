using AutoMapper;
using Hugger_Application.Models;
using Hugger_Application.Models.GoogleDriveAPI;
using Hugger_Application.Models.Repository;
using Hugger_Application.Models.UserDTO;
using Hugger_Application.Services;
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
    /// Accounts contoller.
    /// Responsible for user register, login, and token handling
    /// </summary>
    [ApiController]
    [Route("hugger/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Contructor for AccountController
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public AccountController(IUserRepository repository, IUserService userService, IMapper mapper, ILogger<AccountController> logger)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Authenticate user by model which include login and password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /user to test auth
        ///     {
        ///         "login": "test",
        ///         "password": "test"
        ///     }
        /// 
        /// </remarks>
        /// <param name="userLogin"></param>
        /// <returns>Returns logged user with token </returns>
        /// <response code="200">Return logger user with token</response> 
        /// <response code="400">User login/password not match/response>
        /// <response code="500">Server not responding</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginDTO>> PostLogin(UserAuthDTO userLoginModel)
        {
            _logger.LogInformation($"POST log in userLogin= {userLoginModel.Login}");
            try
            {
                var loggedUser = await _userService.LogInUserAsync(userLoginModel.Login, userLoginModel.Password);
                if (loggedUser == null) return BadRequest("Username or password is not correct");

                return Ok(_mapper.Map<UserLoginDTO>(loggedUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }

        /// <summary>
        /// Create new user record in database
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Uploads record to database</returns>
        /// <response code="201">User created</response>
        /// <response code="400">User login or id in use</response> 
        /// <response code="500">Server not responding</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<UserGetDTO>> PostNewUser(UserRegisterDTO userModel)
        {
            _logger.LogInformation($"POST new user");
            try
            {
                var existingUser = await _repository.GetUserByLoginAsync(userModel.Login);
                if (existingUser != null) return BadRequest($"{userModel.Login} in use");

                existingUser = await _repository.GetUserByIDAsync(userModel.Id);
                if (existingUser != null) return BadRequest($"{userModel.Id} in use");

                _logger.LogInformation($"Mapping user");
                var user = _mapper.Map<User>(userModel);
                _repository.CreateUser(user);

                if (await _repository.SaveChangesAsync()) return Created($"hugger/users/{user.Id}", _mapper.Map<UserGetDTO>(user));
                else return BadRequest("Failed to add new user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }

        }







    }
}
