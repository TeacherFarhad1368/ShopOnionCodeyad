using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.UserPanel.Wallet;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;
using Transactions.Application.Contract;
using Users.Application.Contract.WalletApplication.Command;
using ShopBoloor.WebApplication.Utility;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Shared.Application;
using ZarinPal.Class;
using Dto.Payment;

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
        private readonly SiteData _data;
        public WalletController(IAuthService authService,IUserPanelWalletQuery userPanelWalletQuery,
            IWalletApplication walletApplication, ITransactionApplication transactionApplication,
            IOptions<SiteData> option)
        {
            _authService = authService;
            _userPanelWalletQuery = userPanelWalletQuery;
            _transactionApplication = transactionApplication;
            _walletApplication = walletApplication;
            _data = option.Value;
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
            
                CreateTransaction createTransaction = new CreateTransaction()
                {
                    OwnerId = 0,
                    Portal = portal.Value,
                    Price = price,
                    TransactionFor = TransactionFor.Wallet,
                    UserId = _userId
                };
                var result = await _transactionApplication.CreateAsync(createTransaction);
                if (result.Success)
                {
                    switch (portal)
                    {
                        case TransactionPortal.زرین_پال:
                            var payment = new ZarinPal.Class.Payment();
                            var resultPayment = await payment.Request(new DtoRequest
                            {
                                Amount = price,
                                CallbackUrl = $"{_data.SiteUrl}Wallet/Payment/{result.Id}",
                                Description = "شارژ کیف پول",
                                MerchantId = _data.MerchentZarinPall
                            }, Payment.Mode.sandbox);
                            if(resultPayment.Status == 100)
                                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{resultPayment.Authority}");
                            break;
                    }
                    TempData["FaildCreateWallet"] = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["FaildCreateWallet"] = true;
                    return RedirectToAction("Index");
                }
            
        }
    }
}
