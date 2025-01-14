using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductApplication
{
    public interface IProductQuery
    {
        Task<int> GetProductIdByProductSellIdAsync(int productSellId);
        List<ProductForAddProductSellQueryModel> GetProductsForAddProductSells(int id);
        ProductAdminPaging GetProductsForAdmin(int pageId, int take, int categoryId, string filter, ProductAdminOrderBy orderBy);
    }
    public class ProductForAddProductSellQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public enum ProductAdminOrderBy
    {
        تاریخ_ثبت_از_اول,
        تاریخ_ثبت_از_آخر,
    }
    public class ProductAdminPaging : BasePaging
    {
        public int CategoryId { get; set; }
        public string Filter { get; set; }
        public string PageTitle { get; set; }
        public ProductAdminOrderBy OrderBy { get; set; }
        public List<ProductQueryAdminModel> Products { get; set; }
    }
    public class ProductQueryAdminModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }
        public int Weight { get; set; }
        public bool Active { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
    }
}
