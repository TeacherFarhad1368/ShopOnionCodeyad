using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Seller;
using Shared.Application;
using Shared.Domain.Enum;
using Shop.Application.Contract.SellerApplication.Command;
using Shop.Application.Contract.SellerApplication.Query;
using ShopBoloor.WebApplication.Utility;
using System.ComponentModel.DataAnnotations;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Seller
{
    [Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_فروشگاه_ها)]
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
        public IActionResult Index(int pageId = 1,int take = 10,string filter = "")
        {
            var model = _sellerAdminQuery.GetSellersForAdmin(pageId, take, filter);
            return View(model);
        }
        public IActionResult Requests()
        {
            var model = _sellerAdminQuery.GetSellerRequestsForAdmin();
            return View(model);
        }
        public IActionResult Request(int id)
        {
            var model = _sellerAdminQuery.GetSellerRequestDetailForAdmin(id);
            if(model.Status == SellerStatus.درخواست_تایید_شده)
            {
                return Redirect($"/Admin/Seller/Detail/{id}");
            }
            return View(model); 
        }
        public IActionResult Detail(int id)
        {
            var model = _sellerAdminQuery.GetSellerDetailForAdmin(id);
            if (model.Status == SellerStatus.درخواست_تایید_نشده || model.Status == SellerStatus.درخواست_ارسال_شده)
            {
                return Redirect($"/Admin/Seller/Request/{id}");
            }
            return View(model);
        }
        public IActionResult Accept(int id) => PartialView("Accept", new ChangeSellerStatusByAdmin()
        {
            Id = id
        });
        [HttpPost]
        public async Task<JsonResult> Accept(ChangeSellerStatusByAdmin model)
        {
            if (string.IsNullOrEmpty(model.DescriptionSMS))
            {
                OperationResult res = new(false, "لطفا متن اس ام اس را وارد کنید .");
                return Json(res);
            }
            else
            {
                bool ok = await _sellerApplication.ChangeStatusAsync(model.Id, SellerStatus.درخواست_تایید_شده);

                OperationResult res = new OperationResult(ok, ok ? "عملیات موفق" : "عملیات نا موفق . تماس با برنامه نویس ...");
                return Json(res);
            }
        }
        public IActionResult Reject(int id) => PartialView("Reject", new ChangeSellerStatusByAdmin()
        {
            Id = id
        });
        [HttpPost]
        public async Task<JsonResult> Reject(ChangeSellerStatusByAdmin model)
        {
            if (string.IsNullOrEmpty(model.DescriptionSMS))
            {
                OperationResult res = new(false,"لطفا متن اس ام اس را وارد کنید .");
                return Json(res);
            }
            else
            {
                bool ok = await _sellerApplication.ChangeStatusAsync(model.Id, SellerStatus.درخواست_تایید_نشده);

                OperationResult res = new OperationResult(ok, ok ? "عملیات موفق" : "عملیات نا موفق . تماس با برنامه نویس ...");
                return Json(res);
            }
        }
    }
    public class ChangeSellerStatusByAdmin
    {
        public int Id { get; set; }
        [Display(Name = "متن اس ام اس")]
        [Required(ErrorMessage = "متن اس ام اس الزامی است .")]
        public string DescriptionSMS { get; set; }
        [Display(Name = "متن ایمیل")]
        public string? DescriptionEmail { get; set; }
    }
}
