using System.Globalization;
using lecreventAPI.Models;
using lecreventAPI.Services.UserService;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _userService.RegisterUser(user);
            return Ok(result);
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