using Shared.DTOs;
using Shared.Helpers;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Helpers;

namespace UserService.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtHelper _jwtHelper;
        public UsersService(IUsersRepository userRepository, PasswordHasher passwordHasher, JwtHelper jwtHelper)
        {
            _usersRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }


        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            // Kiểm tra theo Email
            var user = await _usersRepository.GetByEmailAsync(loginDto.Identifier);
            var user1 = user;
            if (user != null && user.EmailConfirmed == true && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                return new LoginResponseDTO
                {
                    Success = true,
                    Token = _jwtHelper.GenerateToken(user, user.RoleId),
                    UserId = user.UserId.ToString(),
                    Username = user.Username,
                    Role = user.RoleId
                };
            }

            // Kiểm tra theo Username
            user = await _usersRepository.GetByUsernameAsync(loginDto.Identifier);
            if (user != null && user.EmailConfirmed == true && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                return new LoginResponseDTO
                {
                    Success = true,
                    Token = _jwtHelper.GenerateToken(user, user.RoleId),
                    UserId = user.UserId.ToString(),
                    Username = user.Username,
                    Role = user.RoleId
                };
            }

            // Trả về thất bại nếu không tìm thấy user hoặc mật khẩu sai
            return new LoginResponseDTO
            {
                Success = false,
                ErrorMessage = "Invalid username/email or password"
            };
        }

        public async Task<LoginResponseDTO> LoginWithGoogleAsync(LoginGoogleDTO request)
        {
            var user = await _usersRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                // Tạo tài khoản mới nếu chưa tồn tại
                user = new User
                {
                    RoleId = "6807a3224dc09155c419126d", // RoleId của "Reader"
                    Avatar = "https://res.cloudinary.com/dzdbjmycj/image/upload/v1744558664/default-avatar_usptvx.avif", // Giá trị mặc định
                    FullName = request.FullName,
                    Username = request.Email,
                    Password = _passwordHasher.HashPassword("Password"),
                    Email = request.Email,
                    CreatedDate = DateTime.UtcNow,
                    UpToAuthor = false,
                    Otp = "none",
                    IsDeleted = false
                };

                await _usersRepository.CreateAsync(user);
            }

            return new LoginResponseDTO
            {
                Success = true,
                Token = _jwtHelper.GenerateToken(user, user.RoleId),
                UserId = user.UserId.ToString(),
                Username = user.Username,
                Role = user.RoleId
            };
        }

        public async Task<User?> RegisterAsync(RegisterDTO registerDto)
        {
            var existingUserByEmail = await _usersRepository.GetByEmailAsync(registerDto.Email);
            var existingUserByUsername = await _usersRepository.GetByUsernameAsync(registerDto.Username);

            // Nếu email đã xác thực -> không cho đăng ký
            if (existingUserByEmail != null && existingUserByEmail.EmailConfirmed == true)
                return null;

            // Nếu username đã xác thực -> không cho đăng ký
            if (existingUserByUsername != null && existingUserByUsername.EmailConfirmed == true)
                return null;

            // Nếu Email tồn tại nhưng chưa xác thực -> cập nhật thông tin và trả về
            if (existingUserByEmail != null && !existingUserByEmail.EmailConfirmed == true)
            {
                existingUserByEmail.Username = registerDto.Username;
                existingUserByEmail.FullName = registerDto.FullName;
                existingUserByEmail.Password = _passwordHasher.HashPassword(registerDto.Password);
                existingUserByEmail.CreatedDate = DateTime.UtcNow;
                existingUserByEmail.UpToAuthor = registerDto.UpToAuthor;

                await _usersRepository.UpdateAsync(existingUserByEmail.UserId, existingUserByEmail);
                return existingUserByEmail;
            }

            // Nếu Username tồn tại nhưng chưa xác thực -> cập nhật thông tin và trả về
            if (existingUserByUsername != null && !existingUserByUsername.EmailConfirmed == true)
            {
                existingUserByUsername.Email = registerDto.Email;
                existingUserByUsername.FullName = registerDto.FullName;
                existingUserByUsername.Password = _passwordHasher.HashPassword(registerDto.Password);
                existingUserByUsername.CreatedDate = DateTime.UtcNow;
                existingUserByUsername.UpToAuthor = registerDto.UpToAuthor;

                await _usersRepository.UpdateAsync(existingUserByUsername.UserId, existingUserByUsername);
                return existingUserByUsername;
            }

            // Nếu chưa tồn tại -> tạo user mới
            var user = new User
            {
                RoleId = "6807a3224dc09155c419126d",
                Avatar = "123", // mặc định
                FullName = registerDto.FullName,
                Username = registerDto.Username,
                Password = _passwordHasher.HashPassword(registerDto.Password),
                Email = registerDto.Email,
                CreatedDate = DateTime.UtcNow,
                UpToAuthor = registerDto.UpToAuthor,
                IsDeleted = false
            };

            await _usersRepository.CreateAsync(user);
            return user;
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Password = _passwordHasher.HashPassword(user.Password);
            return await _usersRepository.CreateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _usersRepository.DeleteAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _usersRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _usersRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _usersRepository.GetByEmailAsync(email);
        }

        public async Task RestoreAsync(string id)
        {
            await _usersRepository.RestoreAsync(id);
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            user.Password = _passwordHasher.HashPassword(user.Password);
            return await _usersRepository.UpdateAsync(id, user);
        }

        public async Task<string> GenerateOtpAsync(string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null; // User not found
            }

            // Store OTP and creation time in the user
            user.Otp = OtpHelper.GenerateOtp();
            user.OtpCreatedAt = DateTime.UtcNow;
            await _usersRepository.UpdateAsync(user.UserId, user);

            return user.Otp;
        }

        public async Task<bool> ConfirmOtpAsync(string email, string otp)
        {
            // Find the user by email
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return false; // User not found
            }

            // Check if OTP exists and is not expired
            if (string.IsNullOrEmpty(user.Otp) || user.OtpCreatedAt == null)
            {
                return false; // No OTP found
            }

            // Check OTP expiration (2 minutes = 120 seconds)
            var timeElapsed = (DateTime.UtcNow - user.OtpCreatedAt.Value).TotalSeconds;
            if (timeElapsed > 120)
            {
                // Clear the OTP after expiration
                user.Otp = null;
                user.OtpCreatedAt = null;
                await _usersRepository.UpdateAsync(user.UserId, user);
                return false; // OTP expired
            }

            // Verify OTP
            if (user.Otp != otp)
            {
                return false; // Invalid OTP
            }

            // OTP is valid, clear it after successful verification
            user.Otp = null;
            user.OtpCreatedAt = null;
            user.EmailConfirmed = true;
            await _usersRepository.UpdateAsync(user.UserId, user);

            return true;
        }
    }
}
