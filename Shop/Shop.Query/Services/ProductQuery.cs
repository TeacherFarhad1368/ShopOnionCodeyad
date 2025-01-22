using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shop.Application.Contract.ProductApplication;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Services
{
    internal class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;

        public ProductQuery(ShopContext context)
        {
            _context = context;
        }

        public async Task<int> GetProductIdByProductSellIdAsync(int productSellId)
        {
            var sell = await _context.ProductSells.FindAsync(productSellId);
            return sell.ProductId;
        }

        public List<ProductForAddProductSellQueryModel> GetProductsForAddProductSells(int id)
        {
            return _context.Products.Include(c=>c.ProductCategoryRelations)
                .Where(c=>c.ProductCategoryRelations.Any(r=>r.ProductCategoryId == id))
                .Select(c=> new ProductForAddProductSellQueryModel
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList();
        }
         
       
        public async Task<bool> IsProductSellForUserAsync(int productSellId, int userId)
        {
            var sell = await _context.ProductSells.Include(p => p.Seller).SingleOrDefaultAsync(p => p.Id == productSellId);
            return sell != null && sell.Seller.UserId == userId;
        }
    }
}
