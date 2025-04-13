using Shared.DTOs;
using Shared.EmailService.EmailTemplates;
using Shared.Helpers;
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

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            // Kiểm tra theo Email
            var user = await _usersRepository.GetByEmailAsync(loginDto.Identifier);
            if (user != null && user.EmailConfirmed == true && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                return new LoginResponseDTO
                {
                    Success = true,
                    Token = _jwtHelper.GenerateToken(user, user.Role.RoleName),
                    UserId = user.UserId.ToString(),
                    Username = user.Username,
                    Role = user.Role.RoleName
                };
            }

            // Kiểm tra theo Username
            user = await _usersRepository.GetByUsernameAsync(loginDto.Identifier);
            if (user != null && user.EmailConfirmed == true && _passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                return new LoginResponseDTO
                {
                    Success = true,
                    Token = _jwtHelper.GenerateToken(user, user.Role.RoleName),
                    UserId = user.UserId.ToString(),
                    Username = user.Username,
                    Role = user.Role.RoleName
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
                    UserId = Guid.NewGuid(),
                    RoleId = 4, // RoleId của "Reader"
                    Avatar = "123", // Giá trị mặc định
                    FullName = request.FullName,
                    Username = request.Email,
                    Password = _passwordHasher.HashPassword("Password"),
                    Email = request.Email,
                    CreatedDate = DateTime.UtcNow,
                    UpToAuthor = false,
                    IsDeleted = false // hoặc role mặc định
                };

                await _usersRepository.CreateAsync(user);
            }

            return new LoginResponseDTO
            {
                Success = true,
                Token = _jwtHelper.GenerateToken(user, user.Role.RoleName),
                UserId = user.UserId.ToString(),
                Username = user.Username,
                Role = user.Role.RoleName
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

                await _usersRepository.UpdateAsync(existingUserByEmail);
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

                await _usersRepository.UpdateAsync(existingUserByUsername);
                return existingUserByUsername;
            }

            // Nếu chưa tồn tại -> tạo user mới
            var user = new User
            {
                UserId = Guid.NewGuid(),
                RoleId = 4,
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


        public async Task<User?> GetByIdAsync(Guid userId) => await _usersRepository.GetByIdAsync(userId);
        public async Task<User?> GetByUsernameAsync(string username) => await _usersRepository.GetByUsernameAsync(username);
        public async Task<User?> GetByEmailAsync(string email) => await _usersRepository.GetByEmailAsync(email);
        public IQueryable<User> GetUsersQueryable() => _usersRepository.GetUsersQueryable();
        public async Task<List<User>> GetAllUsersAvailableAsync() => await _usersRepository.GetAllUsersAvailableAsync();
        public async Task<User> CreateAsync(User user)
        {
            user.UserId = Guid.NewGuid(); // Sinh UserId mới
            user.Password = _passwordHasher.HashPassword(user.Password);
            return await _usersRepository.CreateAsync(user);
        }
        public async Task<User?> UpdateAsync(Guid userId, User updatedUser)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            // Cập nhật RoleId nếu được cung cấp
            if (updatedUser.RoleId != 0)
                user.RoleId = updatedUser.RoleId;
            user.FullName = updatedUser.FullName;
            user.Avatar = updatedUser.Avatar;
            if (!string.IsNullOrEmpty(updatedUser.Password))
                user.Password = _passwordHasher.HashPassword(updatedUser.Password);
            user.Email = updatedUser.Email;
            user.EmailConfirmed = updatedUser.EmailConfirmed;
            user.Phone = updatedUser.Phone;
            user.PhoneConfirmed = updatedUser.PhoneConfirmed;
            user.Gender = updatedUser.Gender;
            user.Bio = updatedUser.Bio;
            user.DateOfBirth = updatedUser.DateOfBirth;
            user.UpToAuthor = updatedUser.UpToAuthor;
            user.ReportCount = updatedUser.ReportCount;
            user.IsDeleted = updatedUser.IsDeleted;
            user.Address = updatedUser.Address;
            user.IdentificationNumber = updatedUser.IdentificationNumber;


            await _usersRepository.UpdateAsync(user);
            return user;
        }

        public async Task<User?> RestoreAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            // Chỉ cần cập nhật IsDeleted thành false
            user.IsDeleted = false;

            await _usersRepository.UpdateAsync(user);
            return user;
        }

        public async Task DeleteAsync(Guid userId) => await _usersRepository.DeleteAsync(userId);

        public async Task<string> GenerateOtpAsync(string email)
        {
            // Find the user by email
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null; // User not found
            }

            // Generate OTP
            string otp = OtpHelper.GenerateOtp();

            // Store OTP and creation time in the user
            user.Otp = otp;
            user.OtpCreatedAt = DateTime.UtcNow;
            await _usersRepository.UpdateAsync(user);

            return otp;
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
                await _usersRepository.UpdateAsync(user);
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
            await _usersRepository.UpdateAsync(user);

            return true;
        }
    }
}
