using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PostModule.Application.Contract.PostCalculate;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UserPanel.Address;
using Query.Contract.UserPanel.Order;
using Query.Contract.UserPanel.Seller;
using Query.Contract.UserPanel.User;
using Query.Contract.UserPanel.Wallet;
using Shared.Application;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.OrderApplication.Query;
using Shop.Application.Contract.ProductSellApplication.Command;
using ShopBoloor.WebApplication.Utility;
using Stores.Application.Contract.StoreApplication.Command;
using System.Net;
using System.Text;
using System.Text.Json;
using Transactions.Application.Contract;
using Users.Application.Contract.UserAddressApplication.Command;
using Users.Application.Contract.WalletApplication.Command;

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
        private readonly IOrderDiscountApplication _orderDiscountApplication;
        private readonly IUserAddressUserPanelQuery _userAddressUserPanelQuery;
        private readonly IUserAddressApplication _userAddressApplication;
        private readonly IStateQuery _stateQuery;
        private readonly IPostCalculateApplication _postCalculateApplication;
        private readonly SiteData _data;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IUserPanelWalletQuery _userPanelWalletQuery;
        private readonly IWalletApplication _walletApplication;
        private readonly IUserPanelQuery _userPanelQuery;
        private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
        private readonly IStoreApplication _storeApplication;
        private readonly IProductSellApplication _productSellApplication;
        public OrderController(IAuthService authService, IOrderApplication orderApplication, IOrderQuery orderQuery,
            IOrderUserPanelQuery orderUserPanelQuery, IOrderDiscountApplication orderDiscountApplication, IUserPanelQuery userPanelQuery,
            IUserAddressUserPanelQuery userAddressUserPanelQuery, IUserAddressApplication userAddressApplication,
            IStateQuery stateQuery, IPostCalculateApplication postCalculateApplication, ITransactionApplication transactionApplication,
            IOptions<SiteData> option, IUserPanelWalletQuery userPanelWalletQuery, IWalletApplication walletApplication,
            ISellerUserPanelQuery sellerUserPanelQuery,IStoreApplication storeApplication,IProductSellApplication productSellApplication)
        {
            _authService = authService;
            _orderApplication = orderApplication;
            _orderQuery = orderQuery;
            _orderUserPanelQuery = orderUserPanelQuery; 
            _orderDiscountApplication = orderDiscountApplication;
            _userAddressUserPanelQuery = userAddressUserPanelQuery; 
            _userAddressApplication = userAddressApplication;   
            _stateQuery = stateQuery;
            _postCalculateApplication = postCalculateApplication;
            _transactionApplication = transactionApplication;
            _data = option.Value;
            _userPanelWalletQuery = userPanelWalletQuery;   
            _walletApplication = walletApplication;
            _userPanelQuery = userPanelQuery;
            _sellerUserPanelQuery = sellerUserPanelQuery;
            _storeApplication = storeApplication;
            _productSellApplication = productSellApplication;
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
            await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            await _orderApplication.CheckOrderEmpty(_userId);
            var res = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            if(res == null)
            {
                TempData["noOpenOrder"] = true;
                return Redirect("/Shop");
            }
            return View(res);
        }
        public async Task<bool> DeleteOrderItem(int id)
        {
            _userId = _authService.GetLoginUserId();
            return await _orderApplication.DeleteOrderItemAsync(id,_userId);
        }
        public async Task<IActionResult> OrderItemMinus(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.OrderItemMinus(id,_userId);
            var model = JsonSerializer.Serialize(res);
            return Json(model);
        }
        public async Task<IActionResult> OrderItemPlus(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.OrderItemPlus(id, _userId);
            var model = JsonSerializer.Serialize(res);
            return Json(model);
        }
        [HttpPost]
        public async Task<JsonResult> AddOrderSellerDiscount(int id,string code)
        {
            OperationResult model = new(false);
            _userId = _authService.GetLoginUserId();
            OperationResultWithKey result = await
                _orderUserPanelQuery.HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(_userId, id);
            if(result.Success != false)
            {
                OperationResultOrderDiscount res = await _orderDiscountApplication.GetOrderDiscountForAddOrderSellerdiscountAsync(id, code);
                if(res.Success)
                {
                  bool ok =  await _orderApplication.AddOrderSellerDiscountAsync(_userId, id, res.Id, res.Title, res.Percent);
                    if (ok)
                    {
                        model.Success = true;
                        model.Message = res.Message;
                    }
                    else
                    {
                        await _orderDiscountApplication.MinusUseAsync(res.Id);
                        model.Message = "عملیات نا موفق !! مجددا تلاش کنید ";
                        
                    }
                }
                else
                {
                    model.Message = res.Message;
                }
            }
            else
            {
                model.Message = result.Message;
            }
            var json = JsonSerializer.Serialize(model);
            return Json(json);
        }
        [HttpPost]
        public async Task<JsonResult> AddOrderDiscount(string code)
        {
            OperationResult model = new(false);
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if (ok)
            {
                OperationResultOrderDiscount res = await _orderDiscountApplication.GetOrderDiscountForAddOrderdiscountAsync(code);
                if (res.Success)
                {
                    bool add = await _orderApplication.AddOrderDiscountAsync(_userId, res.Id, res.Title, res.Percent);
                    if(add)
                    {
                        model.Success = true;
                        model.Message = res.Message;
                    }
                    else
                    {
                        await _orderDiscountApplication.MinusUseAsync(res.Id);
                        model.Message = "عملیات نا موفق !! مجددا تلاش کنید ";
                    }
                }
                else
                    model.Message = res.Message;
            }
            else
                model.Message = "شما فاکتور بازی ندارید .";
            var json = JsonSerializer.Serialize(model);
            return Json(json);
        }
        public async Task<JsonResult> OpenOrderItems()
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
            await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            await _orderApplication.CheckOrderEmpty(_userId);
            var res = await _orderUserPanelQuery.GetOpenOrderItemsAsync(_userId);
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<JsonResult> AddOrderItem(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.AddOrderItemAsync(_userId, id);
            if(res.Success) await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<IActionResult> AddOrderAddress()
        {
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if(ok)
            {
                var model = _userAddressUserPanelQuery.GetAddresseForUserPanel(_userId);
                return PartialView("AddOrderAddress", model);
            }
            return NotFound();
        }
        [HttpPost] 
        public async Task<IActionResult> AddOrderAddress(CreateAddress model)
        {
            OperationResult res = new(false);
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if (!ok)
            {
                res.Message = "شما فاکتور بازی ندارید .";
            }
            else
            {
                if (_stateQuery.IsStateCorrect(model.StateId) == false)
                    res.Message = "لطفا یک استان انتخاب کنید";
                else if (_stateQuery.IsCityCorrect(model.StateId, model.CityId) == false)
                    res.Message = "لطفا یک شهر انتخاب کنید";
                else
                {
                    res = _userAddressApplication.Create(model, _userId);
                    if (res.Success)
                        res = await _orderApplication.CreateOrderAddressAsync(new CreateOrderAddress
                        {
                            AddressDetail = model.AddressDetail,
                            CityId = model.CityId,
                            FullName = model.FullName,
                            IranCode = model.IranCode,
                            Phone = model.Phone,    
                            PostalCode = model.PostalCode,
                            StateId = model.StateId,
                        }, _userId);
                }
            }
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<bool> ChangeOrderAddress(int id)
        {
            _userId = _authService.GetLoginUserId();
            bool isAddressForUser = await _userAddressApplication.IsAddressForUser(id,_userId);
            if (!isAddressForUser) return false;
            CreateAddress model = await _userAddressApplication.GetAddressForAddToFactor(id);
            var res = await _orderApplication.CreateOrderAddressAsync(new CreateOrderAddress
            {
                AddressDetail = model.AddressDetail,
                CityId = model.CityId,
                FullName = model.FullName,
                IranCode = model.IranCode,
                Phone = model.Phone,
                PostalCode = model.PostalCode,
                StateId = model.StateId,
            }, _userId);
            return res.Success;
        }
        public async Task<JsonResult> CalculatePostPrice(int id)
        {
            PostResposeModel model = new() { success = false ,sellerId = id};
            _userId = _authService.GetLoginUserId();
            var res = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            if(res == null)
                model.message = "";
            else
            {
                if (res.OrderAddress == null || res.OrderAddress.CityId == 0)
                    model.message = "برای محاسبه قیمت پست ابتدا آدرس را وارد کنید .";
                else
                {
                    var orderseller = res.Ordersellers.SingleOrDefault(s => s.Id == id);
                    if (orderseller == null) model.message = "";
                    else
                    {
                        int sourceCity = await _orderUserPanelQuery.GetCityOfSeller(orderseller.SellerId);
                        if (sourceCity == 0) model.message = "";
                        else
                        {
                            int weight = await _orderUserPanelQuery.CalculateOrdersellerWeight(id);
                            if (weight == 0) model.message = "";
                            else
                            {
                                var posts = await _postCalculateApplication.CalculatePost(new PostPriceRequestModel
                                {
                                    DestinationCityId = res.OrderAddress.CityId,
                                    SourceCityId = sourceCity,
                                    Weight = weight
                                });
                                model.success = true;
                                model.posts = posts;    
                            }
                        }
                    }
                }
            } 
            var json = JsonSerializer.Serialize(model);
            return Json(json);
        }
        [HttpPost]
        public async Task<bool> ChoosePostPrice(int id,int post,string postTitle)
        {
            _userId = _authService.GetLoginUserId();
            var res = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            if (res == null) return false;
            else
            {
                if (res.OrderAddress == null || res.OrderAddress.CityId == 0) return false;
                else
                {
                    var orderseller = res.Ordersellers.SingleOrDefault(s => s.Id == id);
                    if (orderseller == null) return false;
                    else
                    {
                        int sourceCity = await _orderUserPanelQuery.GetCityOfSeller(orderseller.SellerId);
                        if (sourceCity == 0) return false;
                        else
                        {
                            int weight = await _orderUserPanelQuery.CalculateOrdersellerWeight(id);
                            if (weight == 0) return false;
                            else
                            {
                                if (post == 0)
                                    if (!string.IsNullOrEmpty(postTitle))
                                        return await _orderApplication.AddPostToOrdersellerAsync(_userId, id, 0, 0, postTitle);
                                    else return false;
                                var posts = await _postCalculateApplication.CalculatePost(new PostPriceRequestModel
                                {
                                    DestinationCityId = res.OrderAddress.CityId,
                                    SourceCityId = sourceCity,
                                    Weight = weight
                                });
                                var postChoose = posts.SingleOrDefault(s => s.PostId == post);
                                if(postChoose == null) return false;
                                bool ok = await _orderApplication.AddPostToOrdersellerAsync(_userId, id, postChoose.PostId, postChoose.Price, postChoose.Title);
                                return ok;
                            }
                        }
                    }
                }
            }
        }
        public async Task<JsonResult> ChangePayment(OrderPayment? pay)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = new(false);
            if (pay == null)
                res.Message = "";
            else
            {
                res = await _orderApplication.ChangeOrderPayment(_userId ,pay.Value);  
            }
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<IActionResult> PaymentFactor()
        {
            _userId = _authService.GetLoginUserId();
            OperationResultfactor res = new(false);
            var model = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            if (model.OrderAddress == null) res.Message = "آدرس را وارد کنید ";
            else
            {
                bool ok = true;
                foreach (var item in model.Ordersellers)
                    if (string.IsNullOrEmpty(item.PostTitle))
                    {
                        res.Message = "برای هر فاکتور فروشنده پست را انتخاب کنید .";
                        ok = false;
                    }
                if (ok)
                {
                    if(model.OrderPayment == OrderPayment.پرداخت_از_درگاه)
                    {
                        var user = _userPanelQuery.GetUserInfoForPanel(_userId);

                        var requestZarinPalUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
                        ZarinPalRequestModel data = new ZarinPalRequestModel
                        {
                            amount = model.PaymentPrice,
                            callback_url = $"{_data.SiteUrl}Wallet/Payment",
                            currency = "IRT",
                            description = "شارژ کیف پول",
                            merchant_id = _data.MerchentZarinPall,
                            mobile = user.Mobile
                        };
                        using (WebClient client = new WebClient())
                        {
                            // تنظیم هدرها (اگر لازم است)
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            string jsonData = JsonSerializer.Serialize(data);
                            // تبدیل داده‌های JSON به بایت‌ها
                            byte[] requestData = Encoding.UTF8.GetBytes(jsonData);

                            // ارسال درخواست POST و دریافت پاسخ
                            byte[] responseData = client.UploadData(requestZarinPalUrl, "POST", requestData);

                            // تبدیل پاسخ به رشته
                            string responseString = Encoding.UTF8.GetString(responseData);
                            ZarinPalResponseModel response = JsonSerializer.Deserialize<ZarinPalResponseModel>(responseString);

                            if (response.data.code == 100 && response.data.message.ToLower() == "success")
                            {
                                CreateTransaction createTransaction = new CreateTransaction()
                                {
                                    OwnerId = model.OrderId,
                                    Portal = TransactionPortal.زرین_پال,
                                    Price = model.PaymentPrice,
                                    TransactionFor = TransactionFor.Order,
                                    UserId = _userId,
                                    Authority = response.data.authority
                                };
                                var result = await _transactionApplication.CreateAsync(createTransaction);
                                res.Success = true;
                                res.Message = "در حال انتقال به درگاه ";
                                res.Url = $"https://sandbox.zarinpal.com/pg/StartPay/{response.data.authority}";
                            }
                            else
                            {

                                res.Message = "خطای درگاه پرداخت !!! پس از دقایقی مجددا تلاش کنید .";
                            }
                        }
                    }
                    else
                    {
                        int walletUserAmount = _userPanelWalletQuery.GetUserWalletAmount(_userId);
                        if (walletUserAmount < model.PaymentPrice)
                        {
                            int price = model.PaymentPrice - walletUserAmount;
                            res.Message = $"کیف پول شما مبلغ {price.ToString("#,0")}کم دارد لطفا کیف پول خور را شارژ کنید .";
                            res.Url = "/UserPanel/Wallet/Index";
                        }
                        else
                        {
                           var result = await _walletApplication.WithdrawalAsync(new CreateWalletWithWhy
                            {
                                Description = $"پرداخت فاکتور شماره {model.OrderId}",
                                Price = model.PaymentPrice,
                                UserId = _userId,
                                WalletWhy = WalletWhy.خرید_از_سایت
                            });
                            if (result.Success)
                            {
                                var orderId = await _orderApplication.PaymentSuccessOrderAsync(_userId, model.PaymentPrice);
                                if (orderId > 0)
                                {
                                    await CheckProductAmoutsAfterPaymentAsync(orderId);
                                    res.Success = true;
                                    res.Message = "فاکتور با موفقیت از کیف پول شما پرداخت شد ";
                                    res.Url = $"/UserPanel/Order/Detail/{model.OrderId}";
                                }
                                else
                                {
                                    res.Message = "خطای سیستم !! تماس با سایت .";
                                }
                            }
                            else
                            {
                                res.Message = "خطای سیستم !! تماس با سایت .";
                            }
                        }
                    }
                }
            }
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public IActionResult Detail(int id)
        {
            _userId = _authService.GetLoginUserId();
            var model = _orderUserPanelQuery.GetOrderDetailForUserPanel(id, _userId);
            if (model == null) return NotFound();
            if (model.OrderStatus == OrderStatus.پرداخت_نشده) return RedirectToAction("Order");
            return View(model); 
        }
        public IActionResult Orders(int pageId = 1)
        {
            _userId = _authService.GetLoginUserId();
            var model = _orderUserPanelQuery.GetOrdersForUserPanel(_userId, pageId, 15);
            return View(model);
        }

        public async Task CheckProductAmoutsAfterPaymentAsync(int orderId)
        {
            var model = _orderUserPanelQuery.GetOrderDetailForUserPanel(orderId);
            foreach (var orderSeller in model.OrderSellers)
            {
                var userId = _sellerUserPanelQuery.GetSellerUserId(orderSeller.SellerId);
                CreateStore res = new()
                {
                    Description = $"پرداخت فاکتور شماره {orderSeller.Id}",
                    SellerId = orderSeller.SellerId,
                    Products = new List<CreateStoreProduct>()
                };
                foreach (var item in orderSeller.OrderItems)
                {
                    CreateStoreProduct create = new()
                    {
                        Count = item.Count,
                        ProductSellId = item.ProductSellId,
                        Type = StoreProductType.کاهش
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
    }
    public class PostResposeModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int sellerId { get; set; }
        public List<PostPriceResponseModel> posts { get; set; }
    }
}
