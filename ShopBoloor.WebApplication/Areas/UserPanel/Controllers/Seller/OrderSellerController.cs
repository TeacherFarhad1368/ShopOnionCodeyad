using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Order;
using Query.Contract.UserPanel.Order.Seller;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.ProductSellApplication.Command;
using Shop.Domain.OrderAgg;
using Stores.Application.Contract.StoreApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;

[Area("UserPanel")]
[Authorize]
public class OrderSellerController : Controller
{
    private int _userId;
    private readonly IAuthService _authService;
    private readonly IOrderSellerUserPanelQuery _orderSellerUserPanelQuery;
    private readonly IOrderApplication _orderApplication;
    private readonly IStoreApplication _storeApplication;
    private readonly IProductSellApplication _productSellApplication;
    public OrderSellerController(IAuthService authService, IOrderSellerUserPanelQuery orderSellerUserPanelQuery,
        IOrderApplication orderApplication, IStoreApplication storeApplication, IProductSellApplication productSellApplication)
    {
        _authService = authService;
        _orderSellerUserPanelQuery = orderSellerUserPanelQuery;
        _orderApplication = orderApplication;
        _storeApplication = storeApplication;
        _productSellApplication = productSellApplication;   
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
        var ok = await _orderApplication.ChnageOrderSellerStatusBySellerAsync(id,status,_userId);
        if(status == OrderSellerStatus.لغو_شده_توسط_فروشنده)
            await CheckProductAmoutsAfterPaymentAsync(id, _userId);
        return ok;
    }
    public async Task CheckProductAmoutsAfterPaymentAsync(int orderSellerId,int userId)
    {
        var model = _orderSellerUserPanelQuery.GetOrderSellerDetailForSellerPanel(orderSellerId, userId);
        CreateStore res = new()
        {
            Description = $"لغو فاکتور شماره {model.Id} توسط فروشنده",
            SellerId = model.SellerId,
            Products = new List<CreateStoreProduct>()
        };
        foreach (var item in model.OrderItems)
        {
            CreateStoreProduct create = new()
            {
                Count = item.Count,
                ProductSellId = item.ProductSellId,
                Type = StoreProductType.افزایش
            };
            res.Products.Add(create);
        }
        var result = await _storeApplication.CreateAsync(userId, res);
        if (result.Success)
        {
            await _productSellApplication.EditProductSellAmountAsync(res.Products.Select(r => new EditProdoctSellAmount
            {
                count = r.Count,
                SellId = r.ProductSellId,
                Type = r.Type
            }).ToList());
        }
    }
}
