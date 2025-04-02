using UserService.DTOs;
using UserService.Helpers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtHelper _jwtHelper;

        public UsersService(IUsersRepository usersRepository, PasswordHasher passwordHasher, JwtHelper jwtHelper)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            User? user = null;
            if (loginDto.Identifier.Contains("@"))
                user = await _usersRepository.GetByEmailAsync(loginDto.Identifier);
            else
                user = await _usersRepository.GetByUsernameAsync(loginDto.Identifier);

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.Password))
                throw new Exception("Invalid credentials");

            return _jwtHelper.GenerateToken(user, user.Role.RoleName);
        }

        public async Task<User> RegisterAsync(RegisterDTO registerDto)
        {
            var existingUser = await _usersRepository.GetByUsernameAsync(registerDto.Username) ??
                               await _usersRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null) throw new Exception("Username or email already exists");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                RoleId = Guid.Parse("6CDE3E2A-E1B4-4A5F-8479-A733F7AFC83D"), // RoleId của "User"
                Avatar = "123",
                FullName = registerDto.FullName,
                Username = registerDto.Username,
                Password = _passwordHasher.HashPassword(registerDto.Password),
                Email = registerDto.Email,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _usersRepository.CreateAsync(user);
            return user;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public async Task<User> UpdateAsync(Guid userId, User userInput)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.FullName = userInput.FullName;
            user.Email = userInput.Email;
            user.Phone = userInput.Phone;
            user.Bio = userInput.Bio;

            await _usersRepository.UpdateAsync(user);
            return user;
        }

        public async Task BanUserAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.IsDeleted = true;
            await _usersRepository.UpdateAsync(user);
        }
    }
}
