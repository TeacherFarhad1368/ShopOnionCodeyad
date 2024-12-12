using Shared;
using Shared.Domain.Enum;
using Stores.Application.Contract.StoreApplication.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.Store
{
    public interface IStoreUserPanelQuery
    {
        bool CheckCreateStore(CreateStore model, int userId);
        List<ProductForAddStoreQueryModel> GetSellerProductsForCreateStore(int id, int userId);
        List<SellerForStoresUserPanelQueryModel> GetSellersForCreateStore(int userId);
        StoreDetailForSellerPanelQueryModel GetStoreDetailForSellerPanel(int userId, int id);
        StoreUserPanelPaging GetStoresForUserPanel(int userId,int sellerId,string filter,int pageId,int take);
    }
    public class StoreDetailForSellerPanelQueryModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string SellerTitle { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public List<StoreProductDetailForSellerPanelQueryModel> StoreProducts { get; set; }
    }
    public class StoreProductDetailForSellerPanelQueryModel
    {
        public int Id { get; set; }
        public int ProductSellId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Unit { get; set; }
        public StoreProductType Type { get; set; }
        public int Count { get; set; }
        public string ProductImageName { get; set; }
    }
        public class ProductForAddStoreQueryModel
    {
        public int ProductId { get; set; }
        public int ProductSellId { get; set; }
        public string ProductTitle { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
    }
    public class StoreUserPanelPaging : BasePaging
    {
        public int SellerId { get; set; }
        public string PageTitle { get; set; }
        public string Filter { get; set; }
        public List<StoreUserPanelQueryModel> Stores { get; set; }
        public List<SellerForStoresUserPanelQueryModel> Sellers { get; set; }
    }
    public class SellerForStoresUserPanelQueryModel
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
    }
    public class StoreUserPanelQueryModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string CreationDate { get; set; }
    }
}
