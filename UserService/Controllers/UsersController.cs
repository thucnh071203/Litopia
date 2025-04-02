using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Models;
using UserService.Services.Interfaces;

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
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetAllAvailable")]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAllAvailable()
        {
            var users = await _usersService.GetAllUsersAvailableAsync();
            return Ok(users);
        }

        [HttpGet("GetById/{userId}")]
        //[Authorize]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("Update/{userId}")]
        //[Authorize]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UserDTO userDto)
        {
            var updatedUser = await _usersService.UpdateAsync(userId, userDto);
            if (updatedUser == null)
                return NotFound("User not found");
            return Ok(updatedUser);
        }

        [HttpDelete("Delete/{userId}")]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _usersService.DeleteAsync(userId);
            return Ok("Delete successfully!");
        }
    }
}
