using Client.ServiceClient;
using Client.ViewModels;
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

        public async Task<IActionResult> Index(string filterType = null)
        {
            string odataQuery = string.Empty;

            switch (filterType?.ToLower())
            {
                case "users-available":
                    odataQuery = "$filter=RoleId eq 4 and IsDeleted eq false";
                    break;
                case "users-banned":
                    odataQuery = "$filter=RoleId eq 4 and IsDeleted eq true";
                    break;
                case "authors-available":
                    odataQuery = "$filter=RoleId eq 3 and IsDeleted eq false";
                    break;
                case "authors-banned":
                    odataQuery = "$filter=RoleId eq 3 and IsDeleted eq true";
                    break;
                case "staff-available":
                    odataQuery = "$filter=RoleId eq 2 and IsDeleted eq false";
                    break;
                case "staff-banned":
                    odataQuery = "$filter=RoleId eq 2 and IsDeleted eq true";
                    break;
                case "uptoauthor-true":
                    odataQuery = "$filter=UpToAuthor eq true";
                    break;
                default:
                    // Nếu không có filter, trả về danh sách rỗng hoặc thông báo
                    return View(new UserListViewModel { Users = new List<UserDTO>(), FilterType = "none" });
            }

            var users = await _usersServiceClient.GetUsersODataAsync(odataQuery);
            var model = new UserListViewModel
            {
                Users = users,
                FilterType = filterType ?? "none"
            };

            return View(model);
        }

    }
}
