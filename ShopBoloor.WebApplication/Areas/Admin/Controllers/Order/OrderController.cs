using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Order;
using Query.Contract.UserPanel.Order.Seller;
using Shared.Domain.Enum;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.ProductSellApplication.Command;
using Shop.Application.Contract.SellerApplication.Query;
using Stores.Application.Contract.StoreApplication.Command;
using Users.Application.Contract.WalletApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Order
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderAdminQuery _orderAdminQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IWalletApplication _walletApplication;
        private readonly ISellerQuery _sellerQuery;
        private readonly IOrderSellerUserPanelQuery _orderSellerUserPanelQuery;
        private readonly IStoreApplication _storeApplication;
        private readonly IProductSellApplication _productSellApplication;

        public OrderController(IOrderAdminQuery orderAdminQuery, IOrderApplication orderApplication, IWalletApplication walletApplication, ISellerQuery sellerQuery, IOrderSellerUserPanelQuery orderSellerUserPanelQuery, IStoreApplication storeApplication, IProductSellApplication productSellApplication)
        {
            _orderAdminQuery = orderAdminQuery;
            _orderApplication = orderApplication;
            _walletApplication = walletApplication;
            _sellerQuery = sellerQuery;
            _orderSellerUserPanelQuery = orderSellerUserPanelQuery;
            _storeApplication = storeApplication;
            _productSellApplication = productSellApplication;
        }

        public IActionResult Index(int pageId = 1,int take = 15, int orderId = 0,int userId = 0, OrderAdminStatus status = OrderAdminStatus.همه)
        {
            var model = _orderAdminQuery.GetOrdersForAdmin(pageId, take, orderId,userId,status);
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var model = _orderAdminQuery.GetOrderDetailForAdmin(id);
            if (model == null) return NotFound();
            return View(model); 
        }
        public async Task<bool> Cancel(int id)
        {
            try
            {
                bool ok = await _orderApplication.CancelByAdminAsync(id);
                if (ok)
                {
                    var model = _orderAdminQuery.GetOrderDetailForAdmin(id);
                    foreach (var item in model.OrderSellers)
                    {
                        if (item.Status != OrderSellerStatus.لغو_شده_توسط_مشتری && item.Status != OrderSellerStatus.لغو_شده_توسط_فروشنده)
                        {
                            int userId = _sellerQuery.GetSellerUserId(item.SellerId);
                            await CheckProductAmoutsAfterPaymentAsync(item.Id, userId);
                            await _walletApplication.DepositForReportOrderSellerAsync(new CreateWallet()
                            {
                                Description = $"لغو فاکتور شماره f_{model.Id}",
                                Price = item.PaymentPrice + item.PostPrice,
                                UserId = model.User.UserId
                            });
                            await _walletApplication.WithdrawForReportOrderSellerAsync(new CreateWallet()
                            {
                                Description = $"لغو فاکتور شماره f_{model.Id}",
                                Price = item.PaymentPrice + item.PostPrice,
                                UserId = userId
                            });
                        }
                    }
                    await _orderApplication.CancelOrderSellersAsync(id);
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async Task CheckProductAmoutsAfterPaymentAsync(int orderSellerId, int userId)
        {
            var model = _orderSellerUserPanelQuery.GetOrderSellerDetailForSellerPanel(orderSellerId, userId);
            CreateStore res = new()
            {
                Description = $"لغو فاکتور شماره {model.Id} توسط فروشنده",
                SellerId = model.SellerId,
                Products = new List<CreateStoreProduct>()
            };
            foreach (var item in model.OrderItems)
            {
                CreateStoreProduct create = new()
                {
                    Count = item.Count,
                    ProductSellId = item.ProductSellId,
                    Type = StoreProductType.افزایش
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
