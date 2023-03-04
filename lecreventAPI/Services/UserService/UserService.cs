using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace lecreventAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>?> DeleteUser(int id)
        {
            var user = await _context.user_profiles.FindAsync(id);
            if (user == null)
                return null;
            _context.user_profiles.Remove(user);
            await _context.SaveChangesAsync();
            return await _context.user_profiles.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.user_profiles.ToListAsync();
            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await _context.user_profiles.FindAsync(id);
            if (user == null)
                return null;
            return user;
        }

        public async Task<List<User>> RegisterUser(User user)
        {
            var existingUser = await _context.user_profiles.FirstOrDefaultAsync(x => x.username == user.username || x.email == user.email);
            if (existingUser != null)
            {
                return null;
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);
            user.password = passwordHash;
            _context.user_profiles.Add(user);
            await _context.SaveChangesAsync();
            return await _context.user_profiles.ToListAsync();
        }

        public async Task<List<User>?> UpdateUser(int id, User request)
        {
            var user = await _context.user_profiles.FindAsync(id);
            if (user == null)
                return null;

            user.firstName = request.firstName;
            user.lastName = request.lastName;
            user.age = request.age;
            user.username = request.username;
            user.password = request.password;
            user.email = request.email;

            await _context.SaveChangesAsync();

            return await _context.user_profiles.ToListAsync();
        }

        public async Task<User> LoginUser(User user)
        {
            var checkUser = await _context.user_profiles.FirstOrDefaultAsync(x => x.username == user.username);
            if (checkUser == null || !BCrypt.Net.BCrypt.Verify(user.password, checkUser.password))
            {
                return null;
            }

            return checkUser;

        }
    }
}