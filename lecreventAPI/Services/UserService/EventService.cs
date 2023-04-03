using Microsoft.EntityFrameworkCore;

namespace lecreventAPI.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly DataContext _context2;
        public EventService(DataContext context)
        {
            _context2 = context;
        }

        public async Task<List<Event>> RegisterEvent(int userId, Event request)
        {
            var newEvent = new Event
            {
                userId = userId,
                description = request.description,
                spaces = request.spaces,
                date = request.date,
                time = request.time,
                latitude = request.latitude,
                longitude = request.longitude,
            };
            _context2.event_details.Add(newEvent);
            await _context2.SaveChangesAsync();
            return await _context2.event_details.ToListAsync();

        }
    }
}