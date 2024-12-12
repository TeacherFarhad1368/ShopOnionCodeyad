using Microsoft.EntityFrameworkCore;
using Query.Contract.UserPanel.Store;
using Shared.Application;
using Shop.Domain.SellerAgg;
using Shop.Infrastructure;
using Stores.Application.Contract.StoreApplication.Command;
using Stores.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Services.UserPanel
{
    internal class StoreUserPanelQuery : IStoreUserPanelQuery
    {
        private readonly ShopContext _shopContext;
        private readonly StoreContext _storeContext;

        public StoreUserPanelQuery(ShopContext shopContext, StoreContext storeContext)
        {
            _shopContext = shopContext;
            _storeContext = storeContext;
        }

        public bool CheckCreateStore(CreateStore model, int userId)
        {
            if (model.Products.Count() < 1) return false;
            var seller = _shopContext.Sellers.Find(model.SellerId);
            if(seller.UserId != userId) return false;
            foreach( var item in model.Products)
            {
                var sellProduct = _shopContext.ProductSells.Find(item.ProductSellId);
                if(sellProduct.SellerId != seller.Id) return false;
            }
            return true;
        }

        public List<ProductForAddStoreQueryModel> GetSellerProductsForCreateStore(int id, int userId)
        {
            var seller = _shopContext.Sellers.Find(id);
            if (seller == null || seller.UserId != userId) return null;
            return _shopContext.ProductSells.Include(s=>s.Product).Where(p=> p.SellerId == id)
                .Select(p=> new ProductForAddStoreQueryModel
                {
                    Price = p.Price,
                    ProductId = p.ProductId,
                    ProductSellId = p.Id,
                    ProductTitle = p.Product.Title,
                    Unit = p.Unit
                }).ToList();
        }

        public List<SellerForStoresUserPanelQueryModel> GetSellersForCreateStore(int userId) =>
            _shopContext.Sellers.Where(s => s.UserId == userId)
                .Select(s => new SellerForStoresUserPanelQueryModel
                {
                    Id = s.Id,
                    SellerName = s.Title
                }).ToList();

        public StoreDetailForSellerPanelQueryModel GetStoreDetailForSellerPanel(int userId, int id)
        {
            var store = _storeContext.Stores.Include(s=>s.StoreProducts).SingleOrDefault(s=>s.Id == id);
            if (store == null || store.UserId != userId) return null;
            StoreDetailForSellerPanelQueryModel model = new StoreDetailForSellerPanelQueryModel()
            {
                CreationDate = store.CreateDate.ToPersainDate(),
                Description = store.Description,
                Id = id,
                SellerId = store.Id,
                SellerTitle = "",
                StoreProducts = store.StoreProducts.Select(s=> new StoreProductDetailForSellerPanelQueryModel
                {
                    Count = s.Count,
                    Id = s.Id,
                    ProductId = 0,
                    ProductSellId = s.ProductSellId,
                    ProductTitle = "",
                    Type = s.Type,
                    Unit = "",
                    ProductImageName = ""
                }).ToList()
            };
            var seller = _shopContext.Sellers.Find(model.SellerId);
            model.SellerTitle = seller.Title;
            model.StoreProducts.ForEach(x =>
            {
                var productsell = _shopContext.ProductSells.Include(s => s.Product).Single(s => s.Id == x.ProductSellId);
                x.ProductId = productsell.ProductId;
                x.ProductTitle = productsell.Product.Title;
                x.Unit = productsell.Unit;
                x.ProductImageName = productsell.Product.ImageName; 
            });
            return model;
        }

        public StoreUserPanelPaging GetStoresForUserPanel(int userId, int sellerId, string filter, int pageId, int take)
        {
            var res = _storeContext.Stores.Where(s => s.UserId == userId).OrderByDescending(o => o.Id);
            if(sellerId > 0)
                res = res.Where(s=>s.SellerId == sellerId).OrderByDescending(o => o.Id);  
            if(!string.IsNullOrEmpty(filter))
                res = res.Where(r=>r.Description.Contains(filter)).OrderByDescending(o => o.Id);
            StoreUserPanelPaging model = new StoreUserPanelPaging();    
            model.GetData(res, pageId, take,2);
            model.Filter = filter;
            model.SellerId = sellerId;
            model.PageTitle = "انبار داری";
            model.Stores = new();
            model.Sellers = new();
            if(res.Count() > 0)
            {
                model.Stores = res.Skip(model.Skip).Take(model.Take)
                    .Select(s => new StoreUserPanelQueryModel
                    {
                        CreationDate = s.CreateDate.ToPersainDate(),
                        Id = s.Id,
                        SellerId = s.SellerId,
                        SellerName = ""
                    }).ToList();

                model.Stores.ForEach(x =>
                {
                    var seller = _shopContext.Sellers.Find(x.SellerId);
                    x.SellerName = seller.Title;
                });
            }
            if(model.SellerId > 0)
            {
                var seller = _shopContext.Sellers.Find(model.SellerId);
                if (seller == null || seller.UserId != userId) return null;
                model.PageTitle = $"انبار داری فروشگاه {seller.Title}";
            }
            model.Sellers = _shopContext.Sellers.Where(s=>s.UserId == userId)
                .Select(s=> new SellerForStoresUserPanelQueryModel
                {
                    Id = s.Id,
                    SellerName = s.Title
                }).ToList();
            return model;   
        }
    }
}
