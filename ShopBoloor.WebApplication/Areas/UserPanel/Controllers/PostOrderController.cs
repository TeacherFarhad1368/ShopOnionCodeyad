using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Application.Services.Auth;
using Query.Contract.UserPanel.PostOrder;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class PostOrderController : Controller
    {
        private int _userId;
        private readonly IPackageApplication _packageApplication;
        private readonly IUserPostApplication _userPostApplication;
        private readonly IPostOrderUserPanelQuery _postOrderUserPanelQuery;
        private readonly IAuthService _authService;
        public PostOrderController(IPackageApplication packageApplication, IUserPostApplication userPostApplication,
            IAuthService authService, IPostOrderUserPanelQuery postOrderUserPanelQuery)
        {
            _authService = authService;
            _userPostApplication = userPostApplication;
            _packageApplication = packageApplication;
            _postOrderUserPanelQuery = postOrderUserPanelQuery;
        }
        public IActionResult Orders(int pageId = 0)
        {
            _userId = _authService.GetLoginUserId();
            var model = _postOrderUserPanelQuery.GetPostOrdersForUsePanel(pageId, _userId);
            return View(model);
        }
        public async Task<IActionResult> Basket()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetPostOrderNotPaymentForUser(_userId);
            if (model == null)
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
        public async Task<IActionResult> Payment()
        {

            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetPostOrderNotPaymentForUser(_userId);
            if (model == null)
            {
                TempData["OrderNotExist"] = true;
                return Redirect("/Post");
            }
            PaymentPostModel payment = new(_userId, 0, model.Price);
            await _userPostApplication.PaymentPostOrderAsync(payment);
            return RedirectToAction("Orders");
        }
        public async Task<IActionResult> UserPost()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetUserPostModelForPanel(_userId);
            return View(model);
        }
    }
}