using Client.ServiceClient;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersServiceClient _usersServiceClient;

        public UsersController(UsersServiceClient usersServiceClient)
        {
            _usersServiceClient = usersServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _usersServiceClient.GetAllUsersAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                // Lưu thông báo lỗi vào ViewData hoặc TempData
                ViewData["ErrorMessage"] = ex.Message;
                return View(new List<UserDTO>());
            }
        }

    }
}
