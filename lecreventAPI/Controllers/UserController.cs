using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace lecreventAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var result = await _userService.GetSingleUser(id);
            if (result is null)
                return NotFound("Nem letezo felhasznalo");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> RegisterUser(User user)
        {
            try
            {
                var result = await _userService.RegisterUser(user);
                if (result is null)
                {
                    return BadRequest("Létező felhasználónév vagy email cim!");
                }
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Létező felhasználónév vagy email cim!");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(User user)
        {
            var result = await _userService.LoginUser(user);

            if (result is null)
            {
                return BadRequest("Helytelen felhasználónév vagy jelszó");
            }

            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(5)
            });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("welcome")]
        [Authorize]
        public async Task<ActionResult<User>> Welcome()
        {
            var userId = int.Parse(User.FindFirstValue("id"));

            var userProfile = await _userService.Welcome(userId);

            return Ok(userProfile);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User request)
        {
            var result = await _userService.UpdateUser(id, request);
            if (result is null)
                return NotFound("Nem letezo felhasznalo");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
                return NotFound("Nem letezo felhasznalo");
            return Ok(result);
        }
    }
}