namespace lecreventAPI.Services.EventService
{
    public interface IEventService
    {
        Task<List<Event>> RegisterEvent(int userId, Event request);
        Task<List<Event>> GetAllEvents();
        Task<List<Event>> GetAllUserEvents(int userId);
    }
}