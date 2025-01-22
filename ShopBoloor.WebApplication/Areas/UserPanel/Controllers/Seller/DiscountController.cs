using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Discounts.Application.Contract.ProductDiscountApplication.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.OrderDiscount;
using Query.Contract.UserPanel.Seller;
using Shared.Application;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
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
        private readonly IOrderDiscountQuery _orderDiscountQuery;
        private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
        private readonly IOrderDiscountApplication _orderDiscountApplication;   
        public DiscountController(IAuthService authService, IProductDiscountApplication 
            productDiscountApplication, ISellerUserPanelQuery sellerUserPanelQuery, IProductQuery 
            productQuery, IOrderDiscountQuery orderDiscountQuery, IOrderDiscountApplication orderDiscountApplication)
        {
            _authService = authService;
            _productDiscountApplication = productDiscountApplication;
            _productQuery = productQuery;
            _orderDiscountQuery = orderDiscountQuery;
            _sellerUserPanelQuery = sellerUserPanelQuery;
            _orderDiscountApplication = orderDiscountApplication;
        }
        public IActionResult Active()
        {
            _userId = _authService.GetLoginUserId();
            List<int> sellerIds = _sellerUserPanelQuery.GetUserSellerIds(_userId);
            var model = _orderDiscountQuery.GetAllActivesForSeller(sellerIds);
            return View(model);
        }
        public IActionResult InActive()
        {
            _userId = _authService.GetLoginUserId();
            List<int> sellerIds = _sellerUserPanelQuery.GetUserSellerIds(_userId);
            var model = _orderDiscountQuery.GetAllInActivesForSeller(sellerIds);
            return View(model);
        }
        public async Task<IActionResult> Create(int id)
        {
            _userId = _authService.GetLoginUserId();
            bool ok = await _productQuery.IsProductSellForUserAsync(id,_userId);
            if(!ok) return NotFound();
            int productId = await _productQuery.GetProductIdByProductSellIdAsync(id);
            CreateProductDiscount model = await _productDiscountApplication.GetForEditAsync(productId, id);
            return PartialView("Create", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateProductDiscount model)
        {
            _userId = _authService.GetLoginUserId();
            bool ok = await _productQuery.IsProductSellForUserAsync(id, _userId);
            if (!ok) return NotFound();
            OperationResult res = new(false);
            if (ModelState.IsValid == false) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
            else
            {
                if(model.ProductSellId != id && model.ProductSellId < 1) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
                else
                {
                    int productId = await _productQuery.GetProductIdByProductSellIdAsync(model.ProductSellId);
                    if(productId != model.ProductId) res.Message = "لطفا اطلاعات را صحیح وارد کنید .";
                    else
                    res = await _productDiscountApplication.CreateProductDiscountAsync(model);
                }
            }
            return Json(JsonConvert.SerializeObject(res));
        }
        public IActionResult CreateDiscount()
        {
            _userId = _authService.GetLoginUserId();
            CreateOrderDiscount model = new();
            return PartialView("CreateDiscount", model);
        }
        [HttpPost]
        public async Task<JsonResult> CreateDiscount(CreateOrderDiscount model,int shopId)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = new(false);
            if (!ModelState.IsValid)
            {
                res.Message = "لطفا اطلاعات را درست وارد کنید .";
                return Json(JsonConvert.SerializeObject(res));
            }
            var seelers = _sellerUserPanelQuery.GetSellersForUserForAddDiscount(_userId);
            if(seelers.Count(x=>x.Id == shopId) == 0)
            {
                res.Message = "لطفا یک فروشگاه انتخاب کنید  .";
                return Json(JsonConvert.SerializeObject(res));
            }
            res = await _orderDiscountApplication.CreateAsync(model, OrderDiscountType.OrderSeller, shopId);
            return Json(JsonConvert.SerializeObject(res));
        }
    }
}
