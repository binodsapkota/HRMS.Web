using HRMS.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HRMS.Application.Interfaces;

namespace HRMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var loginVm = new LoginViewModel() { ReturnUrl = ReturnUrl };
            return View(loginVm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? "/";
            if (!ModelState.IsValid)
                return View(model);

            var user = await _authService.LoginAsync(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in await _authService.GetUserRolesAsync(model.Username))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(model.ReturnUrl ?? "/");
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
