using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The username or password was empty or incorrect", typeof(ErrorResponse))]
        public IActionResult Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Username or password is empty"));
            }

            User user = _userService.Authenticate(username, password);
            if (user == null)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Username or password is incorrect"));
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status201Created, "The user was registered successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The username already existed", typeof(ErrorResponse))]
        public ActionResult<Models.Database.User> Register([FromBody] Models.Database.User user)
        {
            User newUser = _userService.Register(user);
            if (newUser == null)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, $"Username '{user.Username}' already exists"));
            }
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, newUser);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was retrieved successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user ID was not found", typeof(ErrorResponse))]
        public ActionResult<Models.Database.User> GetUser(int id)
        {
            var item = _userService.GetById(id);
            if (item == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"User ID {id} was not found"));
            }
            return item;
        }

        [HttpGet("whoami")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user was retrieved successfully")]
        public ActionResult<Models.Database.User> GetCurrentUser()
        {
            int id = int.Parse(User.Identity.Name);
            var user = _userService.GetById(id);
            return user;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The users were retrieved successfully")]
        public ActionResult<IEnumerable<Models.Database.User>> GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
