using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Shared.DTOs;
using Shared.EmailService;
using Shared.EmailService.EmailTemplates;
using Shared.Hubs;
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Domain.Entities;


namespace UserService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IEmailSender _emailSender;

        public UsersController(IUsersService userService, IEmailSender emailSender)
        {
            _usersService = userService;
            _emailSender = emailSender;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(string? roleId, bool? isDeleted)
        {
            var users = await _usersService.GetAllUsersAsync(roleId, isDeleted);
            return Ok(users);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            return user == null ? NotFound("User not found!") : Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _usersService.CreateAsync(user);

            return Ok(created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var updated = await _usersService.GetByIdAsync(id);
            if (updated == null)
                return NotFound("User not found!");
            updated = await _usersService.UpdateAsync(id, user);

            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found!");
            
            await _usersService.DeleteAsync(id);
            return Ok("Delete successfully!");
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found!");

            await _usersService.RestoreAsync(id);
            return Ok("Delete successfully!");
        }

        [HttpPut("up-to-author/{id}")]
        public async Task<IActionResult> UpgradeToAuthor(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.UpToAuthor = true;
            var updated = await _usersService.UpdateAsync(id, user);

            var content = "<p>Congratulations! Your account has been upgraded to an Author.</p>";
            var html = EmailTemplateHelper.EmailConfirm(user.FullName, content);
            await _emailSender.SendEmailAsync(user.Email, "You're now an Author!", html);

            return Ok(updated);
        }

        [HttpPut("accept-author/{id}")]
        public async Task<IActionResult> AcceptAuthor(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.UpToAuthor = false;
            user.RoleId = "6807a3224dc09155c419126c";
            var updated = await _usersService.UpdateAsync(id, user);

            var content = "<p>Congratulations! Your account has been upgraded to an Author.</p>";
            var html = EmailTemplateHelper.EmailConfirm(user.FullName, content);
            await _emailSender.SendEmailAsync(user.Email, "You're now an Author!", html);

            return Ok(updated);
        }
    }
}
