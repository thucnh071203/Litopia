using Client.ServiceClient.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System.Security.Claims;

namespace Client.Controllers
{
    public class AuthenController : Controller
    {

        private readonly IUsersServiceClient _usersServiceClient;

        public AuthenController(IUsersServiceClient usersService)
        {
            _usersServiceClient = usersService;
        }

        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var response = await _usersServiceClient.LoginAsync(loginDto);

                if (response == null || !response.Success)
                {
                    ModelState.AddModelError("", response?.ErrorMessage ?? "Invalid Login");
                    return View(loginDto);
                }

                // Lưu token vào cookie authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.Username ?? loginDto.Identifier),
                    new Claim(ClaimTypes.Role, response.Role ?? ""),
                    new Claim("Token", response.Token ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, response.UserId ?? "")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginDto);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            try
            {
                var success = await _usersServiceClient.RegisterAsync(registerDto);

                if (success)
                {
                    // Trả về view với thông báo thành công qua TempData
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Registration failed");
                return View(registerDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Xóa cookie xác thực
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Authen");
        }
    }
}
