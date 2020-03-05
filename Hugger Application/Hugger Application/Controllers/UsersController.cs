using System;
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
using Hugger_Application.Data.Repository.UserPrefRepository;
using Microsoft.Extensions.Logging;
using Hugger_Application.Models.UserPreferancesDTO;
using Hugger_Application.Models.UserCharacteristicDTO;
using Hugger_Application.Data.Repository.UserCharRepository;

namespace Hugger_Application.Controllers
{
    /// <summary>
    /// Controller for user accounts.
    /// </summary>
    //[Authorize]
    [Route("hugger/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPrefRepository _userPrefRepository;
        private readonly IUserCharRepository _userCharRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="userPrefRepository"></param>
        /// <param name="userCharRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public UsersController(IUserRepository userRepository, IUserPrefRepository userPrefRepository,
            IUserCharRepository userCharRepository, IMapper mapper,
            ILogger<UsersController> logger,IUserService userService)
        {
            _userRepository = userRepository;
            _userPrefRepository = userPrefRepository;
            _userCharRepository = userCharRepository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
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
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginDTO>> Authenticate(UserAuthDTO authenticateUserModel)
        {
            _logger.LogInformation($"POST authentication for userLogin= {authenticateUserModel.Login}");
            try
            {
                var user = await _userService.LogInUserAsync(authenticateUserModel.Login, authenticateUserModel.Password);
                if (user == null) return BadRequest("Username or password is not correct");
                return Ok(_mapper.Map<UserLoginDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }

        /// <summary>
        /// Get all user from database
        /// </summary>
        /// <returns>Return whole users in database</returns>
        /// <response code="200">Return users</response> 
        /// <response code="500">If server not responding</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRegisterDTO[]>> GetUsers()
        {
            _logger.LogInformation($"GET users from database");
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(_mapper.Map<UserRegisterDTO[]>(users));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
        /// <summary>
        /// Get user but following id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns user by given id</returns>
        /// <response code="200">Return user</response> 
        /// <response code="404">User not found</response> 
        /// <response code="500">Server not responding</response>
        [HttpGet("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<UserRegisterDTO>> Get(int userId)
        {
            _logger.LogInformation($"GET userId= {userId}");
            try
            {
                var user = await _userRepository.GetUserByIDAsync(userId);
                if (user == null)
                {
                    _logger.LogInformation($"User with id= {userId} could not be found");
                    return NotFound($"User with id= {userId} could not be found");
                }
                return Ok(_mapper.Map<UserRegisterDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }

        /// <summary>
        /// Get user preferences by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Preferences of user</returns>
        /// <response code="200">Return user preferences</response> 
        /// <response code="404">Preferences not found</response> 
        /// <response code="500">Server not responding</response>
        [HttpGet("{userId:int}/prefs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPrefGetDTO[]>> GetUserPreferences(int userId)
        {
            _logger.LogInformation($"GET user preferances for userId= {userId}");
            try
            {
                var userPrefs = await _userPrefRepository.GetUserPreferencesByUserIdAsync(userId);
                if (userPrefs == null)
                {
                    _logger.LogInformation($"Userprefs for userId= {userId} not found");
                    return NotFound("User has no preferences");
                }
                return Ok(_mapper.Map<UserPrefGetDTO[]>(userPrefs));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }

        }
        /// <summary>
        /// Get user characteristics by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Characteristics of user</returns>
        /// <response code="200">Return user characteristics</response> 
        /// <response code="404">Characteristics not found</response> 
        /// <response code="500">Server not responding</response>
        [HttpGet("{userId:int}/chars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserCharGetDTO[]>> GetUserCharacteristics(int userId)
        {
            _logger.LogInformation($"GET user characterisitcs for userId= {userId}");
            try
            {
                var userChars = await _userCharRepository.GetUserCharacteristicsByUserIdAsync(userId);
                if (userChars == null)
                {
                    _logger.LogInformation($"UserChars for userId= {userId} not found");
                    return NotFound("User has no characteristics");
                }
                return Ok(_mapper.Map<UserCharGetDTO[]>(userChars));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }


        /// <summary>
        /// Get preference by name
        /// </summary>
        /// <param name="prefName"></param>
        /// <returns>Preferences of users</returns>
        /// <response code="200">Return userPreferences</response> 
        /// <response code="404">UserPreferences not found</response> 
        /// <response code="500">Server not responding</response>

        [HttpGet("prefs/{prefName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPrefGetDTO[]>> GetUserPreferencesName(string prefName)
        {
            _logger.LogInformation($"GET by prefName= {prefName}");
            try
            {
                var prefs = await _userPrefRepository.GetPreferenceByPrefNameAsync(prefName);
                if (prefs == null)
                {
                    _logger.LogInformation($"Preferences of name= {prefName} not found");
                    return NotFound($"Preference of name= {prefName} not found");
                }
                return Ok(_mapper.Map<UserPrefGetDTO[]>(prefs));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }
        /// <summary>
        /// Get characteristic by name
        /// </summary>
        /// <param name="charName"></param>
        /// <returns>Preferences of users</returns>
        /// <response code="200">Return user characteristic</response> 
        /// <response code="404">User characteristic not found</response> 
        /// <response code="500">Server not responding</response>
        [HttpGet("chars/{charName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserCharGetDTO[]>> GetUserCharacteristicsName(string charName)
        {
            _logger.LogInformation($"GET by charName= {charName}");
            try
            {
                var chars = await _userCharRepository.GetCharacteristicByNameAsync(charName);
                if (chars == null)
                {
                    _logger.LogInformation($"Characteristic of name= {charName} not found");
                    return NotFound($"Characteristic of name= {charName} not found");
                }
                return Ok(_mapper.Map<UserCharGetDTO[]>(chars));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }


        /// <summary>
        /// Get user preference byd name and userId
        /// </summary>
        /// <param name="prefId"></param>
        /// <param name="userId"></param>
        /// <returns>Preferences of users</returns>
        /// <response code="200">Return userPreferences</response> 
        /// <response code="404">UserPreferences not found</response> 
        /// <response code="500">Server not responding</response>


        [HttpGet("{userId:int}/prefs/{prefId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPrefGetDTO>> GetUserPrefByIDAndUserId(int prefId, int userId)
        {
            _logger.LogInformation($"GET by prefId= {prefId} and userId= {userId}");
            try
            {
                var userPref = await _userPrefRepository.GetUserPreferenceByID_UserIDAsync(prefId, userId);
                if (userPref == null)
                {
                    _logger.LogInformation($"Preferences of id= {prefId} for userId= {userId} not found");
                    return NotFound($"Preference of id= {prefId} for user not found");
                }
                return Ok(_mapper.Map<UserPrefGetDTO>(userPref));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }

        /// <summary>
        /// Get user characteristic byd name and userId
        /// </summary>
        /// <param name="charId"></param>
        /// <param name="userId"></param>
        /// <returns>Characteristic of users</returns>
        /// <response code="200">Return user characteristic</response> 
        /// <response code="404">User characteristic not found</response> 
        /// <response code="500">Server not responding</response>


        [HttpGet("{userId:int}/chars/{charId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserCharGetDTO>> GetUserPrefByNameAndUserId(int charId, int userId)
        {
            _logger.LogInformation($"GET by charId= {charId} and userId= {userId}");
            try
            {
                var userChar = await _userCharRepository.GetUserCharacteristicByCharID_UserIDAsync(charId, userId);
                if (userChar == null)
                {
                    _logger.LogInformation($"Characteristic of id= {charId} for userId= {userId} not found");
                    return NotFound($"Characteristic of id= {charId} for user not found");
                }
                return Ok(_mapper.Map<UserCharGetDTO>(userChar));
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

        public async Task<ActionResult<UserRegisterDTO>> PostNewUser(UserRegisterDTO userModel)
        {
            _logger.LogInformation($"POST new user");
            try
            {
                var existingUser = await _userRepository.GetUserByLoginAsync(userModel.Login);
                if (existingUser != null) return BadRequest($"{userModel.Login} in use");

                existingUser = await _userRepository.GetUserByIDAsync(userModel.Id);
                if (existingUser != null) return BadRequest($"User existing");

     
                _logger.LogInformation($"Mapping user");
                var user = _mapper.Map<User>(userModel);
                _userRepository.CreateUser(user);

                if (await _userRepository.SaveChangesAsync()) return Created($"hugger/users/{user.Id}", _mapper.Map<UserRegisterDTO>(user));
                else return BadRequest("Failed to add new user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }

        }
        
        /// <summary>
        /// Create new user preference
        /// </summary>
        /// <param name="userPrefModel"></param>
        /// <returns>New userPref to database</returns>
        /// <response code="201">User preference created</response>
        /// <response code="400">User preference exist</response> 
        /// <response code="500">Server not responding</response>
        [HttpPost("newUserPref")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPrefCreateDTO>> PostUserPreferences(UserPrefCreateDTO userPrefModel)
        {
            _logger.LogInformation($"POST new user preference");
            try
            {
                var existingUserPreference = await _userPrefRepository.GetUserPreferenceByID_UserIDAsync(userPrefModel.PreferenceId, userPrefModel.UserId);
                if (existingUserPreference != null) return BadRequest($"This user has this preference");


                _logger.LogInformation($"Mapping user preference");
                var userPref = _mapper.Map<UserPreference>(userPrefModel);
                _logger.LogInformation("Creating new user preference");
                _userPrefRepository.Create(userPref);

                if (await _userPrefRepository.SaveChangesAsync())
                    return Created($"hugger/users/{userPrefModel.UserId}/prefs/{userPrefModel.PreferenceId}",
                        _mapper.Map<UserPrefCreateDTO>(userPrefModel));

                else return BadRequest("Failed to add new user preference");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }

        /// <summary>
        /// Create new user characteristic
        /// </summary>
        /// <param name="userCharCreate"></param>
        /// <returns>New userChar to database</returns>
        /// <response code="201">User characteristic created</response>
        /// <response code="400">User characteristic exist</response> 
        /// <response code="500">Server not responding</response>
        [HttpPost("newUserChar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserCharCreateDTO>> PostUserCharacteristics(UserCharCreateDTO userCharCreate)
        {
            _logger.LogInformation($"POST new user characteristic");
            try
            {
                var existingUserCharacteristic = await _userCharRepository.GetUserCharacteristicByCharID_UserIDAsync(userCharCreate.CharacteristicId, userCharCreate.UserId);
                if (existingUserCharacteristic != null) return BadRequest($"This user has this characteristic");


                _logger.LogInformation($"Mapping user characteristic");
                var userChar = _mapper.Map<UserCharacteristic>(userCharCreate);
                _logger.LogInformation("Creating new user characteristic");
                _userCharRepository.Create(userChar);

                if (await _userPrefRepository.SaveChangesAsync())
                    return Created($"hugger/users/{userCharCreate.UserId}/prefs/{userCharCreate.CharacteristicId}",
                        _mapper.Map<UserCharCreateDTO>(userCharCreate));

                else return BadRequest("Failed to add new user characteristic");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
        }


        /// <summary>
        /// Update/fix whole user data, by certain id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userModel"></param>
        /// <returns>Updated user with new data</returns>
        /// <response code="200">User succesfully updated</response>
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
            _logger.LogInformation($"PUT update/fix for userId= {userId}");
            try
            {
                var exitingUser = await _userRepository.GetUserByIDAsync(userId);
                if (exitingUser == null) return NotFound($"Could not find the user with id {userId}");
                
                _mapper.Map(userModel, exitingUser);

                if (await _userRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserFixDTO>(exitingUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest($"Could not update data for user with id=  {userId}");
        }


        /// <summary>
        /// Update data of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userModel"></param>
        /// <returns>Updated user by giving data</returns>
        /// <response code="200">User succesfully updated</response>
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
            _logger.LogInformation($"PATCH user data for userId= {userId}");
            try
            {
                var exitingUser = await _userRepository.GetUserByIDAsync(userId);
                if (exitingUser == null) return NotFound($"Could not find the user with id {userId}");

                _mapper.Map(userModel, exitingUser);

                if (await _userRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserUpdateDTO>(exitingUser));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest($"Could not update data for user with id=  {userId}");
        }

        /// <summary>
        /// Update user preference value 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="prefId"></param>
        /// <param name="updateDTO"></param>
        /// <returns>Updated user preference value</returns> 
        /// <response code="200">User preference succesfully updated</response>
        /// <response code="400">User preference couldn't be updated</response> 
        /// <response code="404">User preference couldn't be found</response> 
        /// <response code="500">Server not responding</response>
        [HttpPatch("{userId:int}/prefs/{prefId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPrefGetDTO>> PatchUserPref(int userId, int prefId, UserPrefUpdateDTO updateDTO)
        {
            _logger.LogInformation($"PATCH user preference for userID= {userId}");
            try
            {
                var existingUserPref = await _userPrefRepository.GetUserPreferenceByID_UserIDAsync(prefId, userId);
                if (existingUserPref == null) return NotFound($"Could not find the userPreference");

                _mapper.Map(updateDTO, existingUserPref);

                if (await _userPrefRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserPrefGetDTO>(existingUserPref)); 
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest("Could not update data for userPreference");
        }


        /// <summary>
        /// Update user characteristic value 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="charId"></param>
        /// <param name="updateUserCharDTO"></param>
        /// <returns>Updated user characteristic value</returns> 
        /// <response code="200">User characteristic succesfully updated</response>
        /// <response code="400">User characteristic couldn't be updated</response> 
        /// <response code="404">User characteristic couldn't be found</response> 
        /// <response code="500">Server not responding</response>
        [HttpPatch("{userId:int}/chars/{charId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserCharGetDTO>> PatchUserChar(int userId, int charId, UserCharUpdateDTO updateUserCharDTO)
        {
            _logger.LogInformation($"PATCH user characteristic for userID= {userId}");
            try
            {
                var existingUserChar = await _userCharRepository.GetUserCharacteristicByCharID_UserIDAsync(charId, userId);
                if (existingUserChar == null) return NotFound($"Could not find the user characteristic");

                _mapper.Map(updateUserCharDTO, existingUserChar);

                if (await _userCharRepository.SaveChangesAsync()) return Ok(_mapper.Map<UserCharGetDTO>(existingUserChar));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest("Could not update data for userPreference");
        }

        /// <summary>
        /// Delete user with certain id
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204">User succesfully deleted</response>
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
            _logger.LogInformation($"DELETE userID= {userId}");
            try
            {
                var oldUser = await _userRepository.GetUserByIDAsync(userId);
                if (oldUser == null) return NotFound($"Could not find an user with id= {userId}");

                _userRepository.Delete(oldUser);
                if (await _userRepository.SaveChangesAsync()) return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach to database");
            }
            return BadRequest("Failed to delete user");
        }


        //TODO move it to service
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

