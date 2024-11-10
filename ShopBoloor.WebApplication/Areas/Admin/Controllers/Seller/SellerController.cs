using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Seller;
using Shared.Domain.Enum;
using Shop.Application.Contract.SellerApplication.Command;
using Shop.Application.Contract.SellerApplication.Query;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Seller
{
    [Area("Admin")]
    public class SellerController : Controller
    {
        private readonly ISellerAdminQuery _sellerAdminQuery;
        private readonly ISellerQuery _sellerQuery;
        private readonly ISellerApplication _sellerApplication;

        public SellerController(ISellerAdminQuery sellerAdminQuery, ISellerQuery sellerQuery, ISellerApplication sellerApplication)
        {
            _sellerAdminQuery = sellerAdminQuery;
            _sellerQuery = sellerQuery;
            _sellerApplication = sellerApplication;
        }

        public IActionResult Requests()
        {
            var model = _sellerAdminQuery.GetSellerRequestsForAdmin();
            return View(model);
        }
        public IActionResult Request(int id)
        {
            var model = _sellerAdminQuery.GetSellerRequestDetailForAdmin(id);
            return View(model); 
        }
        [HttpPost]
        public async Task<JsonResult> Accept(ChangeSellerStatusByAdmin model)
        {
            var res = await _sellerApplication.ChangeStatusAsync(model.Id, SellerStatus.درخواست_تایید_شده);
            return Json(res);   
        }
        [HttpPost]
        public async Task<JsonResult> Reject(ChangeSellerStatusByAdmin model)
        {
            var res = await _sellerApplication.ChangeStatusAsync(model.Id, SellerStatus.درخواست_تایید_نشده);
            return Json(res);
        }
    }
    public class ChangeSellerStatusByAdmin
    {
        public int Id { get; set; }
        public string DescriptionSMS { get; set; }
        public string DescriptionEmail { get; set; }
    }
}
