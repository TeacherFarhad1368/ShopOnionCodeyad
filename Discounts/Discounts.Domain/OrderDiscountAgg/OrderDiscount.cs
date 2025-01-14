using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discounts.Domain.OrderDiscountAgg
{
    public class OrderDiscount : BaseEntityCreate<int>
    {
        public OrderDiscount(int percent, string title, string code, int count,
            OrderDiscountType type, DateTime startDate, DateTime endDate,int shopId)
        {
            Percent = percent;
            Title = title;
            Code = code;
            Count = count;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            ShopId = shopId;
            Use = 0;
        }
        public void Edit(int percent, string title, string code, int count,
            DateTime startDate, DateTime endDate)
        {
            Percent = percent;
            Title = title;
            Code = code;
            Count = count;
            StartDate = startDate;
            EndDate = endDate;
        }
        public void UsePlus()
        {
            Use = Use + 1;
        }
        public void UseMinus()
        {
            Use = Use - 1;
        }
        public int Percent { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public int Count { get; private set; }
        public OrderDiscountType Type { get; private set; }
        public int ShopId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Use { get; private set; }
    }
}
