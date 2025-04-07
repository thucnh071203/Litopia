using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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

        [HttpGet]
        //[Route("odata/Users")]
        [EnableQuery] // Kích hoạt OData
        //[Authorize(Roles = "Admin,Staff")]
        public IActionResult GetODataUsers()
        {
            var users = _usersService.GetUsersQueryable();
            return Ok(users);
        }

        //[HttpGet("GetAll")]
        ////[Authorize(Roles = "Admin,Staff")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var users = await _usersService.GetAllUsersAsync();
        //    return Ok(users);
        //}

        //[HttpGet("GetAllAvailable")]
        ////[Authorize(Roles = "Admin,Staff")]
        //public async Task<IActionResult> GetAllAvailable()
        //{
        //    var users = await _usersService.GetAllUsersAvailableAsync();
        //    return Ok(users);
        //}

        //[HttpGet("GetAllBanned")]
        ////[Authorize(Roles = "Admin,Staff")]
        //public async Task<IActionResult> GetAllBannedUsersAsync()
        //{
        //    var users = await _usersService.GetAllBannedUsersAsync();
        //    return Ok(users);
        //}

        [HttpGet("GetById/{userId}")]
        //[Authorize]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("Create")]
        //[Authorize]
        public async Task<IActionResult> Create(User user)
        {
            user = await _usersService.CreateAsync(user);
            return Ok(user);
        }

        [HttpPut("Update/{userId}")]
        //[Authorize]
        public async Task<IActionResult> Update(Guid userId, [FromBody] User user)
        {
            var updatedUser = await _usersService.UpdateAsync(userId, user);
            if (updatedUser == null)
                return NotFound("User not found");
            return Ok(updatedUser);
        }

        [HttpPut("Restore/{userId}")]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Restore(Guid userId)
        {
            var restoredUser = await _usersService.RestoreAsync(userId);
            if (restoredUser == null)
                return NotFound("User not found");
            return Ok("User restored successfully!");
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
