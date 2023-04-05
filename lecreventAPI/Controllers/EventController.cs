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
    }
}