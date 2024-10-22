using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discounts.Domain.ProductDiscountAgg
{
    public class ProductDiscount : BaseEntityCreate<int> 
    {
        public ProductDiscount(int productId, int productSellId, DateTime startDate, 
            DateTime endDate, int percent)
        {
            ProductId = productId;
            ProductSellId = productSellId;
            StartDate = startDate;
            EndDate = endDate;
            Percent = percent;
        }
        public void Edit( DateTime startDate,
    DateTime endDate, int percent)
        {
            StartDate = startDate;
            EndDate = endDate;
            Percent = percent;
        }
        public int ProductId { get; private set; }
        public int ProductSellId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Percent { get; private set; }
    }
}
