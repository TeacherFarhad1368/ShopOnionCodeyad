using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Order;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Order
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderAdminQuery _orderAdminQuery;

        public OrderController(IOrderAdminQuery orderAdminQuery)
        {
            _orderAdminQuery = orderAdminQuery;
        }

        public IActionResult Index(int pageId = 1,int take = 15, int orderId = 0,int userId = 0, OrderAdminStatus status = OrderAdminStatus.همه)
        {
            var model = _orderAdminQuery.GetOrdersForAdmin(pageId, take, orderId,userId,status);
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var model = _orderAdminQuery.GetOrderDetailForAdmin(id);
            if (model == null) return NotFound();
            return View(model); 
        }
    }
}
