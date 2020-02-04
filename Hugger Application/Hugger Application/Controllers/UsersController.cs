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

namespace Hugger_Application.Controllers
{
    [Route("hugger/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<User[]>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<User[]>(users);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
