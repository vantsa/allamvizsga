namespace lecreventAPI.Services.EventService
{
    public interface IEventService
    {
        Task<List<Event>> RegisterEvent(int userId, Event request);
    }
}