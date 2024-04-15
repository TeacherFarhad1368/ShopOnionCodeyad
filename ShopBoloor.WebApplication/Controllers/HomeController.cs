using Emails.Application.Contract.EmailUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Site;
using Shared.Application.Services.Auth;
using System.Diagnostics;

namespace ShopBoloor.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailUserApplication _emailUserApplication;
        private readonly IAuthService _authService;
        private readonly ISiteUiQuery _siteUiQuery;
        public HomeController(IEmailUserApplication emailUserApplication, IAuthService authService, ISiteUiQuery siteUiQuery)
        {
            _emailUserApplication = emailUserApplication;
            _authService = authService;
            _siteUiQuery = siteUiQuery;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Page/{slug}")]
        public IActionResult Page(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return NotFound();
            var model = _siteUiQuery.GetSitePageQueryModel(slug);
            if(model == null) return NotFound();
            return View(model);
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