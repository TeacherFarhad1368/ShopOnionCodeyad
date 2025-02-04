using Shared.Domain;
using Shop.Domain.ProductSellAgg;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity<int>
    {
        public int OrderSellerId { get; internal set; }
        public int ProductSellId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int PriceAfterOff { get; private set; }
        public string Unit { get; private set; }
        public OrderSeller OrderSeller { get; private set; }
        public ProductSell ProductSell { get; private set; }
        public OrderItem()
        {
            OrderSeller = new();
            ProductSell = new();
        }
        public void Edit(int count, int price, int priceAfterOff, string unit)
        {
            Count = count;
            Price = price;
            PriceAfterOff = priceAfterOff;
            Unit = unit;
        }
        public OrderItem( int productSellId, int count, int price, int priceAfterOff, string unit)
        {
            ProductSellId = productSellId;
            Count = count;
            Price = price;
            PriceAfterOff = priceAfterOff;
            Unit = unit;
        }
        public void ChangeUnit(string unit)
        {
            Unit = unit;
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
        public void PlusCount(int count)
        {
            Count += count;
        }
        public void MinusCount(int count)
        {
            Count -= count;
        }
    }
}
