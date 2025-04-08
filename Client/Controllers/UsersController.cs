using Client.ServiceClient.Interfaces;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public UsersController(IUsersServiceClient usersServiceClient)
        {
            _usersServiceClient = usersServiceClient;
        }

        public async Task<IActionResult> Index(string filter = null)
        {
            var model = new UserListViewModel
            {
                Users = new List<UserDTO>(),
                FilterType = filter ?? "none"
            };

            try
            {
                string odataQuery = string.Empty;

                switch (filter?.ToLower())
                {
                    case "readers-available":
                        odataQuery = "$filter=RoleId eq 4 and IsDeleted eq false";
                        break;
                    case "readers-banned":
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
                    case "uptoauthor":
                        odataQuery = "$filter=UpToAuthor eq true";
                        break;
                    default:
                        return View(model);
                }

                model.Users = await _usersServiceClient.GetUsersODataAsync(odataQuery);
                return View(model);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Server is under maintenance. Please try again later.";
                return View(model);
            }
        }
    }
}
