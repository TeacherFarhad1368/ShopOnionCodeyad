using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.OrderDiscount;
using Shared.Application;
using Shared.Domain.Enum;
using Users.Application.Contract.WalletApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Discount
{
    [Area("Admin")]
    public class OrderDiscountController : Controller
    {
        private readonly IOrderDiscountApplication _orderDiscountApplication;
        private readonly IOrderDiscountQuery _orderDiscountQuery;

        public OrderDiscountController(IOrderDiscountApplication orderDiscountApplication, IOrderDiscountQuery orderDiscountQuery)
        {
            _orderDiscountApplication = orderDiscountApplication;
            _orderDiscountQuery = orderDiscountQuery;
        }

        public IActionResult Active() => View("Active",_orderDiscountQuery.GetAllActivesForAdmin());
        public IActionResult InActive() => View("Active",_orderDiscountQuery.GetAllInActivesForAdmin());
        public IActionResult Create() => PartialView("Create", new CreateOrderDiscount()
        {
            EndDate = DateTime.Now.AddDays(1).ToPersainDatePicker(),
            StartDate = DateTime.Now.ToPersainDatePicker()
        });
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDiscount model)
        {
            OperationResult res = new(false);
            if (!ModelState.IsValid)
                res = new(false, "لطفا اطلاعات را درست وارد کنید .");
            else
                res = await _orderDiscountApplication.CreateAsync(model,OrderDiscountType.Order,0);
            return Json(JsonConvert.SerializeObject(res));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _orderDiscountApplication.GetForEditAsync(id);
            if (model == null) return NotFound();
            return PartialView("Edit", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditOrderDiscount model)
        {
            var ress = await _orderDiscountApplication.GetForEditAsync(id);
            if (ress == null) return NotFound();
            OperationResult res = new(false);
            if (!ModelState.IsValid || model.Id != id)
                res = new(false, "لطفا اطلاعات را درست وارد کنید .");
            else
                res = await _orderDiscountApplication.EditAsync(model);
            return Json(JsonConvert.SerializeObject(res));
        }
    }
}
