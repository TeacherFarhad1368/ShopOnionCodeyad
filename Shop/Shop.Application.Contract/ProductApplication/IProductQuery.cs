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
        Task<bool> IsProductSellForUserAsync(int productSellId, int userId);
    }
    public class ProductForAddProductSellQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
