using Shared.DTOs;
using UserService.Helpers;
using UserService.Models;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services.Implement
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

        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            // Kiểm tra theo Email
            var user = await _usersRepository.GetByEmailAsync(loginDto.Identifier);
            if (user != null && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
                return _jwtHelper.GenerateToken(user, user.Role.RoleName);

            // Kiểm tra theo Username
            user = await _usersRepository.GetByUsernameAsync(loginDto.Identifier);
            if (user != null && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
                return _jwtHelper.GenerateToken(user, user.Role.RoleName);

            // Trả về null nếu không tìm thấy user hoặc mật khẩu sai
            return null;
        }

        public async Task<User?> RegisterAsync(RegisterDTO registerDto)
        {
            var existingUser = await _usersRepository.GetByUsernameAsync(registerDto.Username) ??
                               await _usersRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return null; // Trả về null nếu username hoặc email đã tồn tại

            var user = new User
            {
                UserId = Guid.NewGuid(),
                RoleId = Guid.Parse("6CDE3E2A-E1B4-4A5F-8479-A733F7AFC83D"), // RoleId của "User"
                Avatar = "123", // Giá trị mặc định
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

        public async Task<User?> GetByIdAsync(Guid userId) => await _usersRepository.GetByIdAsync(userId);

        public async Task<List<User>> GetAllUsersAsync() => await _usersRepository.GetAllUsersAsync();

        public async Task<List<User>> GetAllUsersAvailableAsync() => await _usersRepository.GetAllUsersAvailableAsync();

        public async Task<User?> UpdateAsync(Guid userId, UserDTO userDto)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
                return null; // Trả về null nếu không tìm thấy

            // Ánh xạ dữ liệu từ UserDTO sang User
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Bio = userDto.Bio;
            user.Avatar = userDto.Avatar;
            user.DateOfBirth = userDto.DateOfBirth;
            user.Gender = userDto.Gender;
            user.UpToAuthor = userDto.UpToAuthor;
            user.ReportCount = userDto.ReportCount;
            user.IsDeleted = userDto.IsDeleted;

            // Nếu Password được cung cấp, cập nhật password
            if (!string.IsNullOrEmpty(userDto.Password))
                user.Password = _passwordHasher.HashPassword(userDto.Password);

            // RoleId có thể cập nhật nếu cần (tùy yêu cầu)
            //if (userDto.RoleId != Guid.Empty)
            //    user.RoleId = userDto.RoleId;

            await _usersRepository.UpdateAsync(user);
            return user;
        }

        public async Task DeleteAsync(Guid userId) => await _usersRepository.DeleteAsync(userId);
    }
}
