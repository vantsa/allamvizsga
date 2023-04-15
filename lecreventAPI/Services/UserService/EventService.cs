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

        public async Task<List<Event>> GetAllEvents()
        {
            var today = DateTime.Today;
            var events = await _context2.event_details
                            .OrderBy(e => e.date)
                            .ThenBy(e => e.time)
                            .ToListAsync();
            return events;
        }

        public async Task<List<Event>> GetAllUserEvents(int userId)
        {
            var today = DateTime.Today;
            var events = await _context2.event_details
                            .Where(e => e.userId == userId)
                            .OrderBy(e => e.date)
                            .ThenBy(e => e.time)
                            .ToListAsync();
            return events;
        }
        public async Task<List<Event>?> DeleteEvent(int id)
        {
            var ev = await _context2.event_details.FindAsync(id);
            if (ev == null)
            {
                return null;
            }
            _context2.event_details.Remove(ev);
            await _context2.SaveChangesAsync();
            return await _context2.event_details.ToListAsync();
        }

        public async Task<List<UserEvent>?> RegUserEvent(UserEvent userEvent)
        {
            _context2.user_events.Add(userEvent);
            await _context2.SaveChangesAsync();
            return await _context2.user_events.ToListAsync();
        }
        public async Task<UserEvent?> GetUserEvents(int UserId)
        {
            var userEvent = await _context2.user_events.SingleOrDefaultAsync(x => x.UserId == UserId);
            if (userEvent == null)
            {
                return null;
            }
            return userEvent;
        }
    }
}