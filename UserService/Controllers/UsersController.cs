using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("GetById/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("Update/{userId}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid userId, [FromBody] User user)
        {
            var updatedUser = await _usersService.UpdateAsync(userId, user);
            return Ok(updatedUser);
        }

        [HttpDelete("BanUser/{userId}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> BanUser(Guid userId)
        {
            await _usersService.BanUserAsync(userId);
            return NoContent();
        }
    }
}
