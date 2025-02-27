//using Dto.Response.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;
using Leaf.xNet;
using System.Text.Json;
using Transactions.Application.Contract;
using Users.Application.Contract.WalletApplication.Command;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ShopBoloor.WebApplication.Controllers
{
    public class WalletController : Controller
    {
        private readonly SiteData _data;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IWalletApplication _walletApplication;

        public WalletController(IOptions<SiteData> option, ITransactionApplication transactionApplication, IWalletApplication walletApplication)
        {
            _data = option.Value;
            _transactionApplication = transactionApplication;
            _walletApplication = walletApplication;
        }

        public async Task<IActionResult> Payment( string authority, string status)
        {
            OnlinePaymentViewModel model = new()
            {
                OwnerId = 0,
                RefId = "",
                Success = false,
                TransactionFor = TransactionFor.Wallet
            };
            var transactiom = await _transactionApplication.GetForCheckPaymentAsync(authority);
            if(transactiom == null)
                return View(model);
            else
            {
                var url = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
                Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest();
                model.Price = transactiom.Price;
                model.TransactionFor = transactiom.TransactionFor;
                model.OwnerId = transactiom.OwnerId;
                if (transactiom.Status ==TransactionStatus.نا_موفق)
                {
                    ZarinPalPaymentRequestModel payment = new()
                    {
                        amount = transactiom.Price,
                        authority = authority,
                        merchant_id = _data.MerchentZarinPall
                    };
                    string jsonData = JsonSerializer.Serialize(payment);
                    Leaf.xNet.HttpResponse response = httpRequest.Post(url, jsonData, "application/json");
                    var x = response.StatusCode;
                    //model.RefId = res.RefId.ToString();
                    //if (res.Status == 100)
                    //{
                    //    model.Success = true;
                    //    await _transactionApplication.PaymentAsync(TransactionStatus.موفق, transactiom.Id, res.RefId.ToString());
                    //    switch (transactiom.TransactionFor)
                    //    {
                    //        case TransactionFor.Wallet:
                    //            var create = new CreateWalletWithWhy(transactiom.UserId, transactiom.Price, "شارژ کیف پول", WalletWhy.پرداخت_از_درگاه);
                    //            var resCreateWallet = await _walletApplication.DepositByUserAsync(create);
                    //            await _transactionApplication.AddTransactionWalletId(transactiom.Id, resCreateWallet.Id);
                    //            var wallet = await _walletApplication.GetWalletForCheckPaymentAsync(resCreateWallet.Id);
                    //            model.Description = wallet.Description;

                    //            if (wallet.IsPay == false)
                    //            {
                    //                await _walletApplication.SuccessPaymentAsync(wallet.Id);
                    //            }
                    //            break;
                    //        case TransactionFor.Order:
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    await _transactionApplication.PaymentAsync(TransactionStatus.نا_موفق, transactiom.Id, res.RefId.ToString());
                    //    switch (transactiom.TransactionFor)
                    //    {
                    //        case TransactionFor.Wallet:

                    //            model.Description = "شارژ کیف پول";
                    //            break;
                    //        case TransactionFor.Order:
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                }

                return View(model);
            }
        }
    }
    public class OnlinePaymentViewModel
    {
        public bool Success { get; set; }
        public string RefId { get; set; }
        public string Description { get; set; }
        public TransactionFor TransactionFor { get; set; }
        public int OwnerId { get; set; }
        public int Price { get; set; }
    }
}
