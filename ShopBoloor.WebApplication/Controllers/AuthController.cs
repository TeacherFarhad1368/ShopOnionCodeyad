using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Shared.Application.Services.Auth;
using Users.Application.Contract.UserApplication.Command;

namespace ShopBoloor.WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly IAuthService _authService;
        public AuthController(IUserApplication userApplication, IAuthService authService)
        {
            _userApplication = userApplication;
            _authService = authService;
        }

        public IActionResult Login(string returnUrl = "/")
        {
            RegisterUser model = new() { ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(RegisterUser model)
        {
            if (!ModelState.IsValid) return View(model);
            var ok = _userApplication.Register(model);
            if (ok)
            {
                LoginUser loginModel = new()
                {
                    ReturnUrl = model.ReturnUrl,
                    Mobile = model.Mobile
                };
                return View("LoginUser", loginModel);
            }
            ModelState.AddModelError(nameof(model.Mobile), ValidationMessages.SystemErrorMessage);
            return View(model);
        }
        [HttpPost]
        public IActionResult LoginUser(LoginUser model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = _userApplication.Login(model);
            if(result.Success)
            {
                TempData["SuccessLogin"] = true;
                return Redirect(model.ReturnUrl);
            }
            ModelState.AddModelError(result.ModelName, result.Message);
            return View(model);
        }
        public IActionResult AccessDenied() => View();
        public IActionResult Logout()
        {
            _userApplication.Logout();
            return RedirectToAction("Login");
        }
        public bool IsUserLogin() => _authService.IsUserLogin();    
    }
}
