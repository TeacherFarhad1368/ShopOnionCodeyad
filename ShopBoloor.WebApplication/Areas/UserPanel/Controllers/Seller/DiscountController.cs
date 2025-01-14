using Discounts.Application.Contract.ProductDiscountApplication.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Application;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.ProductApplication;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller
{
    [Area("UserPanel")]
    [Authorize]
    public class DiscountController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IProductDiscountApplication _productDiscountApplication;
        private readonly IProductQuery _productQuery;
        public DiscountController(IAuthService authService, IProductDiscountApplication 
            productDiscountApplication, IProductQuery productQuery)
        {
            _authService = authService;
            _productDiscountApplication = productDiscountApplication;
            _productQuery = productQuery;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id) => PartialView("Create", new CreateProductSellDiscount()
        {
            ProductSellId = id
        });
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateProductSellDiscount model)
        {
            OperationResult res = new(false);
            if (ModelState.IsValid == false) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
            else
            {
                if(model.ProductSellId != id && model.ProductSellId < 1) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
                else
                {
                    int productId = await _productQuery.GetProductIdByProductSellIdAsync(model.ProductSellId);
                    res = await _productDiscountApplication.CreateProductSellDiscountAsync(model, productId);
                }
            }
            return Json(JsonConvert.SerializeObject(res));
        }
    }
}
