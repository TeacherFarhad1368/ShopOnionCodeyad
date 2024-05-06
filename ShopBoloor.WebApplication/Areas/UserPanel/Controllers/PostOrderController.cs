using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Application.Services.Auth;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class PostOrderController : Controller
    {
        private int _userId;
        private readonly IPackageApplication _packageApplication;
        private readonly IUserPostApplication _userPostApplication;
        private readonly IAuthService _authService;
        public PostOrderController(IPackageApplication packageApplication, IUserPostApplication userPostApplication,
            IAuthService authService)
        {
            _authService = authService;
            _userPostApplication = userPostApplication;
            _packageApplication = packageApplication;
        }
        public IActionResult Orders()
        {
            return View();
        }
        public async Task<IActionResult> Basket()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetPostOrderNotPaymentForUser(_userId);
            if(model == null)
            {
                TempData["OrderNotExist"] = true;
                return Redirect("/Post");
            }
            return View(model);
        }
        public async Task<IActionResult> Create(int id)
        {
            _userId = _authService.GetLoginUserId();
            var createpostOrder = await _userPostApplication.GetCreatePostModelAsync(_userId, id);
            await _userPostApplication.CreatePostOrderAsync(createpostOrder);
            return RedirectToAction("Basket");
        }
    }
}
