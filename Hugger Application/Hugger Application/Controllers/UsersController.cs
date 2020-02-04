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

namespace Hugger_Application.Controllers
{
    [Route("hugger/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IUserRepository userRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
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


        //can't add new user with this method, why? Connection pool is the problem?
        public async Task<ActionResult<UserModel>> Post(UserModel userModel)
        {
            try
            {
                //var existingUser = await _userRepository.GetUserByLoginAsync(userModel.Login);
                //if (existingUser != null) return BadRequest($"{userModel.Login} in use");

                /*var location = _linkGenerator.GetPathByAction("Get",
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
    }
}

