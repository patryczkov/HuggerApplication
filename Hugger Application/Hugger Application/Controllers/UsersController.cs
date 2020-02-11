using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hugger_Web_Application.Models;
using Hugger_Application.Models.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Hugger_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Hugger_Application.Services;

namespace Hugger_Application.Controllers
{
    [Authorize]
    [Route("hugger/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IUserRepository userRepository, IMapper mapper, LinkGenerator linkGenerator, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _linkGenerator = linkGenerator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserModel>> Authenticate(AuthenticateUserModel authenticateUserModel)
        {
            var user = await  _userService.AuthenticateAsync(authenticateUserModel.Login, authenticateUserModel.Password);
            if (user == null) return BadRequest("Username or password is not correct");

            return _mapper.Map<UserModel>(user);
        }


        [HttpGet]
        public async Task<ActionResult<UserModel[]>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<UserModel[]>(users);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserModel>> Get(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIDAsync(userId);
                if (user == null) return NotFound($"User with id= {userId} could not be found");

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }

        [HttpPost]

        //linkgenerator not working yet
        public async Task<ActionResult<UserModel>> Post(UserModel userModel)
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

                var user = _mapper.Map<User>(userModel);
                _userRepository.Create(user);

                if (await _userRepository.SaveChangesAsync()) return Created("", _mapper.Map<UserModel>(user));
                return Ok();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }

        }


        [HttpPut("{userId:int}")]
        public async Task<ActionResult<UserModel>> Put(int userId, UserModel userModel)
        {
            try
            {
                var exitingUser = await _userRepository.GetUserByIDAsync(userId);

                if (exitingUser == null)
                {
                    return NotFound($"Could not find the user with id {userId}");
                }

                _mapper.Map(userModel, exitingUser);

                if (await _userRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(exitingUser);
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Could not connect to database");
            }

            return BadRequest();
        }
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<UserModel>> Delete(int userId)
        {
            try
            {
                var oldUser = await _userRepository.GetUserByIDAsync(userId);
                if (oldUser == null) return NotFound($"Could not find an user with id= {userId}");

                _userRepository.Delete(oldUser);
                if (await _userRepository.SaveChangesAsync()) return Ok();
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Could not connect to database");
            }
            return BadRequest("Failed to delete user");
        }
    }
}

