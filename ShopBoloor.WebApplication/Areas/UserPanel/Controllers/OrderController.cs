using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Order;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.OrderApplication.Query;
using System.Text.Json;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IOrderApplication _orderApplication;
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderUserPanelQuery _orderUserPanelQuery;
        public OrderController(IAuthService authService, IOrderApplication orderApplication, IOrderQuery orderQuery,
            IOrderUserPanelQuery orderUserPanelQuery)
        {
            _authService = authService;
            _orderApplication = orderApplication;
            _orderQuery = orderQuery;
            _orderUserPanelQuery = orderUserPanelQuery; 
        }

        public async Task<IActionResult> Order()
        {
            _userId = _authService.GetLoginUserId();
            List<ShopCartViewModel> model = new();
            string cookieName = "boloorShop-cart-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                model = JsonSerializer.Deserialize<List<ShopCartViewModel>>(cartJson);
                var ok = await _orderApplication.UbsertOpenOrderForUserAsync(_userId, model);
                Response.Cookies.Delete(cookieName);
            }
            var res = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            return View(res);
        }
    }
}
