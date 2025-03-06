using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Order.Seller;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
using Shop.Application.Contract.OrderApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;

[Area("UserPanel")]
[Authorize]
public class OrderSellerController : Controller
{
    private int _userId;
    private readonly IAuthService _authService;
    private readonly IOrderSellerUserPanelQuery _orderSellerUserPanelQuery;
    private readonly IOrderApplication _orderApplication;
    public OrderSellerController(IAuthService authService, IOrderSellerUserPanelQuery orderSellerUserPanelQuery, IOrderApplication orderApplication)
    {
        _authService = authService;
        _orderSellerUserPanelQuery = orderSellerUserPanelQuery;
        _orderApplication = orderApplication;
    }

    public IActionResult Index(int pageId = 1)
    {
        _userId = _authService.GetLoginUserId();    
        var model = _orderSellerUserPanelQuery.GetOrderSellersForSeller(_userId,pageId,15);
        return View(model);
    }
    public IActionResult Detail(int id)
    {
        _userId = _authService.GetLoginUserId();
        var model = _orderSellerUserPanelQuery.GetOrderSellerDetailForSellerPanel(id, _userId);
        if (model == null) return NotFound();
        return View(model); 
    }
    public async Task<bool> ChangeStatus(int id,OrderSellerStatus status)
    {
        _userId = _authService.GetLoginUserId();
        return await _orderApplication.ChnageOrderSellerStatusBySellerAsync(id,status,_userId);
    } 
}
