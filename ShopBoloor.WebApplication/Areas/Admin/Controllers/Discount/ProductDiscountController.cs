using Discounts.Application.Contract.ProductDiscountApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Application;
using Shop.Application.Contract.ProductApplication;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Discount
{
    [Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_تخفیفات)]
    public class ProductDiscountController : Controller
    {
        private readonly IProductDiscountApplication _productDiscountApplication;
        private readonly IProductQuery _productQuery;

        public ProductDiscountController(IProductDiscountApplication productDiscountApplication, IProductQuery productQuery)
        {
            _productDiscountApplication = productDiscountApplication;
            _productQuery = productQuery;
        }

        public async Task<IActionResult> Create(int id)
        {
            CreateProductDiscount model = await _productDiscountApplication.GetForEditAsync(id, 0);
            return PartialView("Create", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateProductDiscount model)
        {
            OperationResult res = new(false);
            if (ModelState.IsValid == false) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
            else
            {
                if (model.ProductId != id && model.ProductSellId > 0) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
                else
                {
                    if (id != model.ProductId) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
                    else
                        res = await _productDiscountApplication.CreateProductDiscountAsync(model);
                }
            }
            return Json(JsonConvert.SerializeObject(res));
        }
    }
}
