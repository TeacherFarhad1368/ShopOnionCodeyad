using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Wallet;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
using Transactions.Application.Contract;
using Users.Application.Contract.WalletApplication.Command;
using ShopBoloor.WebApplication.Utility;
using Microsoft.Extensions.Options;
using Query.Contract.UserPanel.User;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class WalletController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IUserPanelWalletQuery _userPanelWalletQuery;
        private readonly IWalletApplication _walletApplication;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IUserPanelQuery _userPanelQuery;
        private readonly SiteData _data;
        public WalletController(IAuthService authService,IUserPanelWalletQuery userPanelWalletQuery,
            IWalletApplication walletApplication, ITransactionApplication transactionApplication,
            IOptions<SiteData> option, IUserPanelQuery userPanelQuery)
        {
            _authService = authService;
            _userPanelWalletQuery = userPanelWalletQuery;
            _transactionApplication = transactionApplication;
            _walletApplication = walletApplication;
            _data = option.Value;
            _userPanelQuery = userPanelQuery;   
        }
        public IActionResult Index(int pageId,string filter)
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelWalletQuery.GetWalletsForUserPanel(_userId,pageId,filter);    
            return View(model);
        }
        public IActionResult Transactions(int pageId, string filter)
        {
            _userId = _authService.GetLoginUserId();
            var model = _userPanelWalletQuery.GetTransactionsForUserPanel(_userId, pageId, filter);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(int price
            ,TransactionPortal? portal) 
        {
            
            if (portal == null)
                portal = TransactionPortal.زرین_پال;
            if (price < 1000)
                return NotFound();
            _userId= _authService.GetLoginUserId();

            var user = _userPanelQuery.GetUserInfoForPanel(_userId);

            var requestZarinPalUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
            ZarinPalRequestModel data = new ZarinPalRequestModel
            {
                amount = price,
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
                        OwnerId = 0,
                        Portal = portal.Value,
                        Price = price,
                        TransactionFor = TransactionFor.Wallet,
                        UserId = _userId,
                        Authority = response.data.authority
                    };
                    var result = await _transactionApplication.CreateAsync(createTransaction);
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{response.data.authority}");
                }
                else
                {

                    TempData["FaildCreateWallet"] = true;
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
