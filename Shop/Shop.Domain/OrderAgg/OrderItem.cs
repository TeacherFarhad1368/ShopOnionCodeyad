using Shared.Domain;
using Shop.Domain.ProductSellAgg;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity<int>
    {
        public int OrderSellerId { get; private set; }
        public int ProductSellId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int PriceAfterOff { get; private set; }
        public OrderSeller OrderSeller { get; private set; }
        public ProductSell ProductSell { get; private set; }
        public OrderItem()
        {
            OrderSeller = new();
            ProductSell = new();
        }
        public void Edit(int count, int price, int priceAfterOff)
        {
            Count = count;
            Price = price;
            PriceAfterOff = priceAfterOff;
        }
        public OrderItem(int orderSellerId, int productSellId, int count, int price, int priceAfterOff)
        {
            OrderSellerId = orderSellerId;
            ProductSellId = productSellId;
            Count = count;
            Price = price;
            PriceAfterOff = priceAfterOff;
        }
        public int SumPrice
        {
            get
            {
                return Count * Price;
            }
        }
        public int SumPriceAfterOff
        {
            get
            {
                return Count * PriceAfterOff;
            }
        }
    }
}
