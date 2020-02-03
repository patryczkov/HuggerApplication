using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hugger_Web_Application.Models;
using Hugger_Application.Models.Repository;

namespace Hugger_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userRepository.FindAll();
            return users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = _userRepository.GetUserByID(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UUID)
            {
                return BadRequest();
            }

            _userRepository.Update(user);

            try
            {
                _userRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userRepository.Create(user);
            _userRepository.Save();

            return CreatedAtAction("GetUser", new { id = user.UUID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = _userRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            _userRepository.Save();

            return user;
        }

        /*private bool UserExists(int id)
        {
            return _userRepository.Any(e => e.UUID == id);
        }*/
    }
}
