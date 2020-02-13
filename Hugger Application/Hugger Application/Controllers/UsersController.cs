using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hugger_Web_Application.Models;
using Hugger_Application.Models.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Hugger_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Hugger_Application.Services;
using Hugger_Application.Models.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using Hugger_Application.Models.GoogleDriveAPI;

namespace Hugger_Application.Controllers
{
    /// <summary>
    /// Controller for user accounts.
    /// </summary>
    [Authorize]
    [Route("hugger/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IUserRepository userRepository, IMapper mapper, IUserService userService, LinkGenerator linkGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _linkGenerator = linkGenerator;
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
        /// <param name="authenticateUserModel"></param>
        /// <returns>Returns logged user with token </returns>
        /// <response code="200">Return logger user with token</response> 
        /// <response code="400">If no user with certain login/password</response>
        /// <response code="500">If server not responding</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRegisterDTO>> Authenticate(AuthenticateUserDTO authenticateUserModel)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(authenticateUserModel.Login, authenticateUserModel.Password);
                if (user == null) return BadRequest("Username or password is not correct");
                return Ok(_mapper.Map<UserRegisterDTO>(user));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }
        /// <summary>
        /// Getting all user from database
        /// </summary>
        /// <returns>Return whole users in database</returns>
        /// <response code="200">Return users</response> 
        /// <response code="500">If server not responding</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRegisterDTO[]>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(_mapper.Map<UserRegisterDTO[]>(users));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }
        /// <summary>
        /// Get user but following id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns user by given id</returns>
        /// <response code="200">Return user</response> 
        /// <response code="404">User not found</response> 
        /// <response code="500">If server not responding</response>
        [HttpGet("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<UserRegisterDTO>> Get(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIDAsync(userId);
                if (user == null) return NotFound($"User with id= {userId} could not be found");

                return Ok(_mapper.Map<UserRegisterDTO>(user));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }
        /// <summary>
        /// Create new user record in database
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Uploads record to database</returns>
        /// <response code="200">When method used without model</response> 
        /// <response code="201">User created</response>
        /// <response code="400">User login or id in use</response> 
        /// <response code="500">Server not responding</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //linkgenerator not working yet
        public async Task<ActionResult<UserRegisterDTO>> PostNewUser(UserRegisterDTO userModel)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByLoginAsync(userModel.Login);
                if (existingUser != null) return BadRequest($"{userModel.Login} in use");

                existingUser = await _userRepository.GetUserByIDAsync(userModel.Id);
                if (existingUser != null) return BadRequest($"{userModel.Id} in use");


                /* var location = _linkGenerator.GetPathByAction("Get",
                     "Users",
                     new { login = userModel.Login });
                 if (string.IsNullOrWhiteSpace(location)) return BadRequest($"{userModel.Login} is not allowed");
                 */

                var gDriveService = ConnectToGDrive.GetDriveService();
                var userFolderPathName = ($"user_{userModel.Login}_photos");
                userModel.FolderPath = userFolderPathName;
                
                var userFolderId = GDriveFolderManagerService.CreateFolder(userFolderPathName, gDriveService);
                userModel.FolderId = userFolderId;

                var user = _mapper.Map<User>(userModel);
                _userRepository.Create(user);

                if (await _userRepository.SaveChangesAsync()) return Created($"hugger/users/{user.Id}", _mapper.Map<UserRegisterDTO>(user));
                return Ok();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }

        }
        /// <summary>
        /// Update/fix whole user data, by certain id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userModel"></param>
        /// <returns>Updated user with new data</returns>
        /// <response code="200">When user succesfully updated</response>
        /// <response code="400">User couldn't be updated</response> 
        /// <response code="404">User couldn't be found</response> 
        /// <response code="500">Server not responding</response>

        [HttpPut("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserFixDTO>> Put(int userId, UserFixDTO userModel)
        {
            try
            {
                var exitingUser = await _userRepository.GetUserByIDAsync(userId);

                if (exitingUser == null) return NotFound($"Could not find the user with id {userId}");

                _mapper.Map(userModel, exitingUser);

                if (await _userRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserFixDTO>(exitingUser));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Could not connect to database");
            }

            return BadRequest($"Could not update data for user with id=  {userId}");
        }

        /// <summary>
        /// Updates some date of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userModel"></param>
        /// <returns>Updated user by giving data</returns>
        /// <response code="200">When user succesfully updated</response>
        /// <response code="400">User couldn't be updated</response> 
        /// <response code="404">User couldn't be found</response> 
        /// <response code="500">Server not responding</response>

        [HttpPatch("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserUpdateDTO>> Patch(int userId, UserUpdateDTO userModel)
        {
            try
            {
                var exitingUser = await _userRepository.GetUserByIDAsync(userId);

                if (exitingUser == null) return NotFound($"Could not find the user with id {userId}");

                _mapper.Map(userModel, exitingUser);

                if (await _userRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserUpdateDTO>(exitingUser));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Could not connect to database");
            }
            return BadRequest($"Could not update data for user with id=  {userId}");
        }

        /// <summary>
        /// Delete user with certain id
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204">When user succesfully deleted</response>
        /// <response code="400">User couldn't be deleted</response> 
        /// <response code="404">User couldn't be found</response> 
        /// <response code="500">Server not responding</response>

        [HttpDelete("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRegisterDTO>> Delete(int userId)
        {
            try
            {
                var oldUser = await _userRepository.GetUserByIDAsync(userId);
                if (oldUser == null) return NotFound($"Could not find an user with id= {userId}");

                _userRepository.Delete(oldUser);
                if (await _userRepository.SaveChangesAsync()) return NoContent();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Could not connect to database");
            }
            return BadRequest("Failed to delete user");
        }
        private bool CheckUserIdAccessLevel(int userId)
        {
            var jwtToken = Request.Headers["Authorization"].ToString().Split(" ")[1];     
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var tokenId = int.Parse(token.Claims.First(c => c.Type == "unique_name").Value);
            return tokenId == userId;
        }
    }
}

