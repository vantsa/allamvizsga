using lecreventAPI.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace lecreventAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<List<Event>>> RegisterEvent(int userId, Event request)
        {
            var result = await _eventService.RegisterEvent(userId, request);
            if(result is null)
                return  BadRequest("Hiba");
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            return await _eventService.GetAllEvents();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Event>>> GetAllUserEvents(int userId)
        {
            return await _eventService.GetAllUserEvents(userId);
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<List<Event>>> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteEvent(id);
            if(result is null)
                return NotFound("Nem letezo esemeny");
            return Ok(result);
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<List<User>>> RegUserEvent(UserEvent userEvent)
        {
            var result = await _eventService.RegUserEvent(userEvent);
            if(result is null)
                return  BadRequest("Hiba");
            return Ok(result);
        }
        [HttpGet("isjoined/{UserId}")]
        public async Task<ActionResult<UserEvent>> GetUserEvents(int UserId)
        {
            var result = await _eventService.GetUserEvents(UserId);
            if (result is null)
            {
                return Ok("nem");
            }
            return Ok(result);
        }
    }
}