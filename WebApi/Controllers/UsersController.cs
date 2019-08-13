using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    /// <summary>
    /// Users Controller Class
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userService">User Service Interface</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate user by user name and password
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>Action Result of User with Authentication Token</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The username or password was empty or incorrect", typeof(ErrorResponse))]
        public async Task<ActionResult<UserVM>> Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Username or password is empty"));
            }

            UserVM user = await _userService.AuthenticateAsync(username, password);
            if (user == null)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Username or password is incorrect"));
            }
            return user;
        }

        /// <summary>
        /// Register user with user name and password, and authenticate automatically upon successful registration
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>Action Result of User with Authentication Token</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status201Created, "The user was registered successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The username already existed", typeof(ErrorResponse))]
        public async Task<ActionResult<UserVM>> Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Username or password is empty"));
            }

            UserVM newUser = await _userService.RegisterAsync(username, password);
            if (newUser == null)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, $"Username '{username}' already exists"));
            }
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        /// <summary>
        /// Get a user by system identifier
        /// </summary>
        /// <param name="id">System User Identifier (not user name)</param>
        /// <returns>Action Result of User</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was retrieved successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user ID was not found", typeof(ErrorResponse))]
        public async Task<ActionResult<UserVM>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"User ID {id} was not found"));
            }
            return user;
        }

        /// <summary>
        /// Get the current authenticated user
        /// </summary>
        /// <returns>Action Result of the Authenticated User</returns>
        [HttpGet("whoami")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was retrieved successfully")]
        public async Task<ActionResult<UserVM>> GetCurrentUser()
        {
            int id = int.Parse(User.Identity.Name);
            return await _userService.GetByIdAsync(id);
        }

        /// <summary>
        /// Get all users in the system
        /// </summary>
        /// <returns>Action Result of a list of Users</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The users were retrieved successfully")]
        public async Task<ActionResult<List<UserVM>>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }
    }
}
