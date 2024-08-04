using Dto.Response.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;
using Transactions.Application.Contract;
using Users.Application.Contract.WalletApplication.Command;
using ZarinPal.Class;
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

        public async Task<IActionResult> Payment(long id, string authority, string status)
        {
            OnlinePaymentViewModel model = new()
            {
                OwnerId = 0,
                RefId = "",
                Success = false,
                TransactionFor = TransactionFor.Wallet
            };
            var transactiom = await _transactionApplication.GetForCheckPaymentAsync(id);
            model.Price = transactiom.Price;
            model.TransactionFor = transactiom.TransactionFor;
            model.OwnerId = transactiom.OwnerId;
            if(transactiom.Status == Shared.Domain.Enum.TransactionStatus.نا_موفق)
            {
                var payment = new Payment();
                Verification res = await payment.Verification(new Dto.Payment.DtoVerification
                {
                    Amount = transactiom.Price,
                    Authority = authority,
                    MerchantId = _data.MerchentZarinPall
                }, ZarinPal.Class.Payment.Mode.sandbox);
                model.RefId = res.RefId.ToString();
                if(res.Status == 100)
                {
                    model.Success = true;
                    await _transactionApplication.PaymentAsync(TransactionStatus.موفق, transactiom.Id, res.RefId.ToString());
                    switch (transactiom.TransactionFor)
                    {
                        case TransactionFor.Wallet:
                            var create = new CreateWalletWithWhy(transactiom.UserId, transactiom.Price, "شارژ کیف پول", WalletWhy.پرداخت_از_درگاه);
                            var resCreateWallet = await _walletApplication.DepositByUserAsync(create);
                            await _transactionApplication.AddTransactionWalletId(transactiom.Id, resCreateWallet.Id);
                            var wallet = await _walletApplication.GetWalletForCheckPaymentAsync(resCreateWallet.Id);
                            model.Description = wallet.Description;

                            if(wallet.IsPay == false)
                            {
                                await _walletApplication.SuccessPaymentAsync(wallet.Id);
                            }
                            break;
                        case TransactionFor.Order:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    await _transactionApplication.PaymentAsync(TransactionStatus.نا_موفق, transactiom.Id, res.RefId.ToString());
                    switch (transactiom.TransactionFor)
                    {
                        case TransactionFor.Wallet:

                            model.Description = "شارژ کیف پول";
                            break;
                        case TransactionFor.Order:
                            break;
                        default:
                            break;
                    }
                }
            }

            return View(model);
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
