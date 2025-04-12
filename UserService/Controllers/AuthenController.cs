using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.EmailService;
using UserService.Helpers;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly PasswordHasher _passwordHasher;
        private readonly IEmailSender _emailSender; // Giả sử bạn có một service

        public AuthenController(IUsersService usersService, PasswordHasher passwordHasher, IEmailSender emailSender)
        {
            _usersService = usersService;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _usersService.LoginAsync(loginDto);

            if (!response.Success)
                return BadRequest(response); // Trả về DTO với ErrorMessage

            return Ok(response); // Trả về DTO với Token và thông tin khác
        }

        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] LoginGoogleDTO request)
        {
            var result = await _usersService.LoginWithGoogleAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var existingUserByEmail = await _usersService.GetByIdAsync(
                (await _usersService.GetAllUsersAvailableAsync()).FirstOrDefault(u => u.Email == registerDto.Email)?.UserId ?? Guid.Empty);
            if (existingUserByEmail != null)
                return Conflict("Email already exists"); // 409 nếu email đã tồn tại

            var existingUserByUsername = await _usersService.GetByIdAsync(
                (await _usersService.GetAllUsersAvailableAsync()).FirstOrDefault(u => u.Username == registerDto.Username)?.UserId ?? Guid.Empty);
            if (existingUserByUsername != null)
                return Conflict("Username already exists"); // 409 nếu username đã tồn tại

            var user = await _usersService.RegisterAsync(registerDto);
            if (user == null)
                return BadRequest("Registration failed"); // 400 nếu đăng ký thất bại

            return Ok(user); // 200 nếu thành công
        }

        [HttpPut("ChangePassword/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordDTO changePasswordDto)
        {
            var user = await _usersService.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            if (!_passwordHasher.VerifyPassword(changePasswordDto.CurrentPassword, user.Password))
                return BadRequest("Current password is incorrect!");

            user.Password = changePasswordDto.NewPassword;
            await _usersService.UpdateAsync(userId, user);
            return Ok("Password changed successfully!");
        }

        [HttpPut("SetPassword/{userId}")]
        public async Task<IActionResult> SetPassword(Guid userId, [FromBody] SetPasswordDTO setPasswordDto)
        {
            var user = await _usersService.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            user.Password = setPasswordDto.NewPassword;
            await _usersService.UpdateAsync(userId, user);
            return Ok("Password set successfully!");
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Please provide To, Subject, and Body");
            }

            try
            {
                await _emailSender.SendEmailAsync(request.ToEmail, request.Subject, request.Body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Email sending failed: {ex.Message}");
            }
        }
    }
}