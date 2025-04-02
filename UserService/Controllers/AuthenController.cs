using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthenController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _usersService.LoginAsync(loginDto);
            if (token == null)
                return BadRequest("Invalid username/email or password!"); // 400 nếu đăng nhập thất bại

            return Ok(new { Token = token }); // 200 nếu thành công
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
    }
}