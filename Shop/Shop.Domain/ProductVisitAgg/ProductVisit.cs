using Shared.Domain;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductVisitAgg
{
    public class ProductVisit : BaseEntity<int>
    {
        public int ProductId { get; private set; }
        public int UserId { get; private set; }
        public int Count { get; private set; }
        public Product Product { get; private set; }
        public ProductVisit()
        {
            Product = new Product();
        }
        public void AddVisit()
        {
            Count++;    
        }
        public ProductVisit(int productId, int userId, int count)
        {
            ProductId = productId;
            UserId = userId;
            Count = count;
        }
    }
}
