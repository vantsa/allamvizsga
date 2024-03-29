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

        public async Task<User?> Welcome(int userId)
        {
            var userProfile = await _context.user_profiles.FindAsync(userId);
            return userProfile;
        }

        public async Task<UserSettings?> GetUserSettings(int userId)
        {
            var userSettings = await _context.user_settings.SingleOrDefaultAsync(us => us.userId == userId);
            if (userSettings == null)
                return null;
            return userSettings;
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

        public async Task<List<UserSettings>?> UpdateSettings(int userId, UserSettings request)
        {
            var userSettings = await _context.user_settings.SingleOrDefaultAsync(us => us.userId == userId);
            if (userSettings == null)
            {
                return null;
            }
            userSettings.value = request.value;
            userSettings.rangeStart = request.rangeStart;
            userSettings.rangeEnd = request.rangeEnd;

            await _context.SaveChangesAsync();

            return await _context.user_settings.ToListAsync();
        }

        public async Task<string> LoginUser(User user)
        {
            var checkUser = await _context.user_profiles.FirstOrDefaultAsync(x => x.username == user.username);
            if (checkUser == null || !BCrypt.Net.BCrypt.Verify(user.password, checkUser.password))
            {
                return null;
            }

            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, checkUser.username)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("lecrevent_vantsa_allamvizsga2k23");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", checkUser.Id.ToString()),
                    new Claim("firstName", checkUser.firstName.ToString()),
                    new Claim("lastName", checkUser.lastName.ToString()),
                    new Claim("username", checkUser.username.ToString()),
                    new Claim("email", checkUser.email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);
            return jwt;

        }
    }
}