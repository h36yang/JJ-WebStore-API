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
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

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

        [HttpGet("whoami")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was retrieved successfully")]
        public async Task<ActionResult<UserVM>> GetCurrentUser()
        {
            int id = int.Parse(User.Identity.Name);
            return await _userService.GetByIdAsync(id);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The users were retrieved successfully")]
        public async Task<ActionResult<List<UserVM>>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }
    }
}
