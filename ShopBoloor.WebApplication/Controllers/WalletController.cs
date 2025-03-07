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
using System.Net;
using System.Text;
using Azure;
using Shop.Application.Contract.OrderApplication.Command;
using Query.Contract.UserPanel.Store;
using Shop.Application.Contract.ProductSellApplication.Command;
using Stores.Application.Contract.StoreApplication.Command;
using Query.Contract.UserPanel.Order;
using Query.Contract.UserPanel.Seller;
namespace ShopBoloor.WebApplication.Controllers
{
    public class WalletController : Controller
    {
        private readonly SiteData _data;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IWalletApplication _walletApplication;
        private readonly IOrderApplication _orderApplication;
        private readonly IOrderUserPanelQuery _orderUserPanelQuery;
        private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
        private readonly IStoreApplication _storeApplication;
        private readonly IProductSellApplication _productSellApplication;
        public WalletController(IOptions<SiteData> option, ITransactionApplication transactionApplication,
            IWalletApplication walletApplication,IOrderApplication orderApplication, IOrderUserPanelQuery orderUserPanelQuery,
            ISellerUserPanelQuery sellerUserPanelQuery, IStoreApplication storeApplication,
            IProductSellApplication productSellApplication)
        {
            _data = option.Value;
            _transactionApplication = transactionApplication;
            _walletApplication = walletApplication;
            _orderApplication = orderApplication;   
            _orderUserPanelQuery = orderUserPanelQuery;
            _sellerUserPanelQuery = sellerUserPanelQuery; 
            _productSellApplication = productSellApplication;   
            _storeApplication = storeApplication;
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
                model.Price = transactiom.Price;
                model.TransactionFor = transactiom.TransactionFor;
                model.OwnerId = transactiom.OwnerId;
                if (transactiom.Status ==TransactionStatus.نا_موفق)
                {
                    var url = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
                    Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest();
                    ZarinPalPaymentRequestModel payment = new()
                    {
                        amount = transactiom.Price,
                        authority = authority,
                        merchant_id = _data.MerchentZarinPall
                    };
                    string jsonData = JsonSerializer.Serialize(payment);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonData);
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            byte[] responseData = client.UploadData(url, "POST", requestData);
                            string responseString = Encoding.UTF8.GetString(responseData);
                            ZarinpalPaymentResponseModel response = JsonSerializer.Deserialize<ZarinpalPaymentResponseModel>(responseString);
                            if (response.data.code == 100 && response.data.message.ToLower() == "paid")
                            {
                                model.Success = true;
                                model.RefId = response.data.ref_id.ToString();
                                await _transactionApplication.PaymentAsync(TransactionStatus.موفق, transactiom.Id, response.data.ref_id.ToString());
                                switch (transactiom.TransactionFor)
                                {
                                    case TransactionFor.Wallet:
                                        var create = new CreateWalletWithWhy(transactiom.UserId, transactiom.Price, "شارژ کیف پول", WalletWhy.پرداخت_از_درگاه);
                                        var resCreateWallet = await _walletApplication.DepositByUserAsync(create);
                                        await _transactionApplication.AddTransactionWalletId(transactiom.Id, resCreateWallet.Id);
                                        var wallet = await _walletApplication.GetWalletForCheckPaymentAsync(resCreateWallet.Id);
                                        model.Description = wallet.Description;

                                        if (wallet.IsPay == false)
                                        {
                                            await _walletApplication.SuccessPaymentAsync(wallet.Id);
                                        }
                                        break;
                                    case TransactionFor.Order:
                                       var orderId = await _orderApplication.PaymentSuccessOrderAsync(transactiom.UserId, transactiom.Price);
                                        await CheckProductAmoutsAfterPaymentAsync(orderId);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                await _transactionApplication.PaymentAsync(TransactionStatus.نا_موفق, transactiom.Id, response.data.ref_id.ToString());
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
                    }
                    catch (Exception)
                    {
                        await _transactionApplication.PaymentAsync(TransactionStatus.نا_موفق, transactiom.Id, "");
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
        public async Task CheckProductAmoutsAfterPaymentAsync(int orderId)
        {
            var model = _orderUserPanelQuery.GetOrderDetailForUserPanel(orderId);
            foreach(var orderSeller in model.OrderSellers)
            {
                var userId = _sellerUserPanelQuery.GetSellerUserId(orderSeller.SellerId);
                CreateStore res = new()
                {
                    Description = $"پرداخت فاکتور شماره {orderSeller.Id}",
                    SellerId = orderSeller.SellerId,
                    Products = new List<CreateStoreProduct>()
                };
                foreach(var item in orderSeller.OrderItems)
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
