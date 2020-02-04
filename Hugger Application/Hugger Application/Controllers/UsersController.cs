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

        //when i am usin user model its fuckingup, but when i am using user it is ok


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
        public async Task<ActionResult<UserModel>> Post(UserModel userModel)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByLoginAsync(userModel.Login);
                if (existingUser != null) return BadRequest($"{userModel.Login} in use");

                /*var location = _linkGenerator.GetPathByAction("Get",
                    "Users",
                    new { login = userModel.Login });
                if (string.IsNullOrWhiteSpace(location)) return BadRequest($"{userModel.Login} is not allowed");
                */
    
                var user = _mapper.Map<User>(userModel);
                _userRepository.Update(user);

                if (await _userRepository.SaveChangesAsync()) return Created("", _mapper.Map<UserModel>(user));
                return Ok();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not contact to database");
            }
        }
    }
}
