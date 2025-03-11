using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Application.Services.Auth;
using Query.Contract.UserPanel.PostOrder;
using Query.Contract.UserPanel.User;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;
using System.Net;
using System.Text;
using Transactions.Application.Contract;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class PostOrderController : Controller
    {
        private int _userId;
        private readonly IPackageApplication _packageApplication;
        private readonly IUserPostApplication _userPostApplication;
        private readonly IPostOrderUserPanelQuery _postOrderUserPanelQuery;
        private readonly IAuthService _authService;
        private readonly IUserPanelQuery _userPanelQuery;
        private readonly SiteData _data;
        private readonly ITransactionApplication _transactionApplication;
        public PostOrderController(IPackageApplication packageApplication, IUserPostApplication userPostApplication,
            IAuthService authService, IPostOrderUserPanelQuery postOrderUserPanelQuery, IUserPanelQuery userPanelQuery,
            IOptions<SiteData> data, ITransactionApplication transactionApplication)
        {
            _authService = authService;
            _userPostApplication = userPostApplication;
            _packageApplication = packageApplication;
            _userPanelQuery = userPanelQuery;
            _data = data.Value;
            _postOrderUserPanelQuery = postOrderUserPanelQuery;
            _transactionApplication = transactionApplication;   
        }
        public IActionResult Orders(int pageId = 0)
        {
            _userId = _authService.GetLoginUserId();
            var model = _postOrderUserPanelQuery.GetPostOrdersForUsePanel(pageId, _userId);
            return View(model);
        }
        public async Task<IActionResult> Basket()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetPostOrderNotPaymentForUser(_userId);
            if (model == null)
            {
                TempData["OrderNotExist"] = true;
                return Redirect("/Post");
            }
            return View(model);
        }
        public async Task<IActionResult> Create(int id)
        {
            _userId = _authService.GetLoginUserId();
            var createpostOrder = await _userPostApplication.GetCreatePostModelAsync(_userId, id);
            await _userPostApplication.CreatePostOrderAsync(createpostOrder);
            return RedirectToAction("Basket");
        }
        public async Task<IActionResult> Payment()
        {

            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetPostOrderNotPaymentForUser(_userId);
            if (model == null)
            {
                TempData["OrderNotExist"] = true;
                return Redirect("/Post");
            }
            var user = _userPanelQuery.GetUserInfoForPanel(_userId);

            var requestZarinPalUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
            ZarinPalRequestModel data = new ZarinPalRequestModel
            {
                amount = model.Price,
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
                        OwnerId = model.Id,
                        Portal = TransactionPortal.زرین_پال,
                        Price = model.Price,
                        TransactionFor = TransactionFor.PostOrder,
                        UserId = _userId,
                        Authority = response.data.authority
                    };
                    var result = await _transactionApplication.CreateAsync(createTransaction);
                   
                    return Redirect( $"https://sandbox.zarinpal.com/pg/StartPay/{response.data.authority}");
                }
                else
                {
                    TempData["ZarrinPalError"] = true;
                    return Redirect("/UserPanel/PostOrder/Basket");
                }
            }
        }
        public async Task<IActionResult> UserPost()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _userPostApplication.GetUserPostModelForPanel(_userId);
            return View(model);
        }
    }
}