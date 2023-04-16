namespace lecreventAPI.Services.EventService
{
    public interface IEventService
    {
        Task<List<Event>> RegisterEvent(int userId, Event request);
        Task<List<Event>> GetAllEvents();
        Task<List<Event>> GetAllUserEvents(int userId);
        Task<List<Event>?> DeleteEvent(int id);
        Task<List<UserEvent>?> RegUserEvent(UserEvent userEvent);
        Task<UserEvent?> GetUserEvents(int UserId);
        Task<List<UserEvent>?> DeleteUserEvent(int Id);

    }
}