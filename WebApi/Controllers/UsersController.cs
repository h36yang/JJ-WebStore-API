using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Authenticate(string username, [FromBody] string password)
        {
            User user = _userService.Authenticate(username, password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Models.Database.User user)
        {
            User newUser = _userService.Register(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, newUser);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Database.User> GetUser(int id)
        {
            var item = _userService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("whoami")]
        public IActionResult GetCurrentUser()
        {
            int id = int.Parse(User.Identity.Name);
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Database.User>> GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
