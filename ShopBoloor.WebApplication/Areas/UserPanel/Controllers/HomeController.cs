using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.User;
using Shared.Application.Services.Auth;
using Users.Application.Contract.UserApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private int _userId;
        private readonly IUserPanelQuery _userPanelQuery;
        private readonly IAuthService _authService;
        private readonly IUserApplication _userApplication;
        public HomeController(IUserPanelQuery userPanelQuery, IAuthService authService, IUserApplication userApplication)
        {
            _userPanelQuery = userPanelQuery;
            _authService = authService;
            _userApplication = userApplication;
        }
        public IActionResult Index()
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelQuery.GetUserInfoForPanel(_userId);
            return View(model);
        }
        public IActionResult EditProfile()
        {
            _userId = _authService.GetLoginUserId();
            var model = _userApplication.GetForEditByUser(_userId);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProfile(EditUserByUser model)
        {
            _userId = _authService.GetLoginUserId();
            if (!ModelState.IsValid) return View(model);
            var res = _userApplication.EditByUser(model, _userId);
            if (res.Success)
            {
                _authService.Logout();
                TempData["SuccessEditProfile"] = true;
                return Redirect("/Auth/Login");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
    }
}
