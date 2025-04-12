using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Shared.DTOs;
using Shared.EmailService;
using Shared.EmailService.EmailTemplates;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IEmailSender _emailSender;
        public UsersController(IUsersService usersService, IEmailSender emailSender)
        {
            _usersService = usersService;
            _emailSender = emailSender;
        }

        [HttpGet]
        [EnableQuery] // Kích hoạt OData
        //[Authorize(Roles = "Admin,Staff")]
        public IActionResult GetODataUsers()
        {
            var users = _usersService.GetUsersQueryable();
            return Ok(users);
        }

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

        [HttpPut("ConfirmEmail/{userId}")]
        public async Task<IActionResult> ConfirmEmail(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            if (user == null) 
                return NotFound("User not found");

            user.EmailConfirmed = true;
            var updated = await _usersService.UpdateAsync(userId, user);

            return Ok(updated);
        }

        [HttpPut("UpgradeToAuthor/{userId}")]
        public async Task<IActionResult> UpgradeToAuthor(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            if (user == null) 
                return NotFound("User not found");

            user.UpToAuthor = true;
            var updated = await _usersService.UpdateAsync(userId, user);

            var content = "<p>Congratulations! Your account has been upgraded to an Author.</p>";
            var html = EmailTemplateHelper.EmailConfirm(user.FullName, content);
            await _emailSender.SendEmailAsync(user.Email, "You're now an Author!", html);

            return Ok(updated);
        }

        [HttpPut("AcceptAuthor/{userId}")]
        public async Task<IActionResult> AcceptAuthor(Guid userId)
        {
            var user = await _usersService.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            user.UpToAuthor = false;
            user.RoleId = 3;
            var updated = await _usersService.UpdateAsync(userId, user);

            var content = "<p>Congratulations! Your account has been upgraded to an Author.</p>";
            var html = EmailTemplateHelper.EmailConfirm(user.FullName, content);
            await _emailSender.SendEmailAsync(user.Email, "You're now an Author!", html);

            return Ok(updated);
        }
    }
}
