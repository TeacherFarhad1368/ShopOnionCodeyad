using Query.Contract.UserPanel.Store;
using Shared.Application;
using Shop.Infrastructure;
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
            return model;   
        }
    }
}
