using Client.ServiceClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.ViewModels;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public UsersController(IUsersServiceClient usersServiceClient)
        {
            _usersServiceClient = usersServiceClient;
        }

        public async Task<IActionResult> Index(string filter = null, string searchString = null)
        {
            var model = new UsersViewModel
            {
                Users = new List<UserDTO>(),
                FilterType = filter ?? "none",
                SearchString = searchString
            };

            try
            {
                string odataQuery = string.Empty;

                switch (filter?.ToLower())
                {
                    case "users-available":
                        odataQuery = "$filter=IsDeleted ne true";
                        break;
                    case "readers-available":
                        odataQuery = "$filter=RoleId eq 4 and IsDeleted ne true";
                        break;
                    case "readers-banned":
                        odataQuery = "$filter=RoleId eq 4 and IsDeleted eq true";
                        break;
                    case "authors-available":
                        odataQuery = "$filter=RoleId eq 3 and IsDeleted ne true";
                        break;
                    case "authors-banned":
                        odataQuery = "$filter=RoleId eq 3 and IsDeleted eq true";
                        break;
                    case "staff-available":
                        odataQuery = "$filter=RoleId eq 2 and IsDeleted ne true";
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
                // Thêm điều kiện tìm kiếm nếu có SearchString
                if (!string.IsNullOrEmpty(searchString))
                {
                    string searchFilter = $"contains(tolower(FullName), '{searchString.ToLower()}') " +
                                         $"or contains(tolower(Address), '{searchString.ToLower()}') " +
                                         $"or contains(tolower(IdentificationNumber), '{searchString.ToLower()}')";

                    if (!string.IsNullOrEmpty(odataQuery))
                    {
                        // Kết hợp filter hiện tại với searchFilter
                        odataQuery = odataQuery.Replace("$filter=", "$filter=(") + ") and (" + searchFilter + ")";
                    }
                    else
                    {
                        odataQuery = $"$filter={searchFilter}";
                    }
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
