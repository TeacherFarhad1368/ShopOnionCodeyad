using Emails.Application.Contract.EmailUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services.Auth;
using System.Diagnostics;

namespace ShopBoloor.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailUserApplication _emailUserApplication;
        private readonly IAuthService _authService;
        public HomeController(IEmailUserApplication emailUserApplication, IAuthService authService)
        {
            _emailUserApplication = emailUserApplication;
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public string AddEmailUser(string email)
        {
            var userId = _authService.GetLoginUserId();
            CreateEmailUser model = new()
            {
                Email = email,
                UserId = userId
            };
            var res = _emailUserApplication.Create(model);
            if (res.Success) return "";
            return res.Message;
        }
    }
}