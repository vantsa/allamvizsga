namespace lecreventAPI.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User>? GetSingleUser(int id);
        Task<List<User>> RegisterUser(User user);
        Task<List<User>?> UpdateUser(int id, User request);
        Task<List<User>?> DeleteUser(int id);
        Task<User?> Welcome(int userId);
        Task<string> LoginUser(User user);
        Task<List<UserSettings>?> UpdateSettings(int userId, UserSettings request);
        Task<UserSettings>? GetUserSettings(int userId);
    }
}