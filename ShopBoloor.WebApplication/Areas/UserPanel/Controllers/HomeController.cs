using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.User;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private int _userId;
        private readonly IUserPanelQuery _userPanelQuery;
        private readonly IAuthService _authService;
        public HomeController(IUserPanelQuery userPanelQuery, IAuthService authService)
        {
            _userPanelQuery = userPanelQuery;
            _authService = authService;
        }
        public IActionResult Index()
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelQuery.GetUserInfoForPanel(_userId);
            return View(model);
        }
    }
}
