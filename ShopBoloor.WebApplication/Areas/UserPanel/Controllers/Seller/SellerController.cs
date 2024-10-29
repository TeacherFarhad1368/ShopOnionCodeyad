using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.SellerApplication.Command;
namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;
[Area("UserPanel")]
[Authorize]
public class SellerController : Controller
{
    private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
    private readonly IAuthService _authService;
    private readonly ISellerApplication _sellerApplication;
    private int _userId;
    public SellerController(ISellerUserPanelQuery sellerUserPanelQuery, IAuthService authService, ISellerApplication sellerApplication)
    {
        _sellerUserPanelQuery = sellerUserPanelQuery;
        _authService = authService;
        _sellerApplication = sellerApplication;
    }

    public IActionResult Index()
    {
        _userId = _authService.GetLoginUserId();
        var model = _sellerUserPanelQuery.GetSellersForUser(_userId);
        return View(model);
    }
    public IActionResult Request() => View();
    [HttpPost]
    public async Task<IActionResult> Request(RequestSeller model)
    {
        _userId = _authService.GetLoginUserId();
        if (!ModelState.IsValid) return View(model);
        var res = await _sellerApplication.RequestSellerAsync(_userId, model);
        if (res.Success)
        {
            TempData["ok"] = true;
            return RedirectToAction("Index");
        }
        ModelState.AddModelError(res.ModelName, res.Message);
        return View(model);
    }
}
