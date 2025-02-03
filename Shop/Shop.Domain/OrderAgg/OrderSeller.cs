﻿using Shared.Domain;
using Shared.Domain.Enum;
using Shop.Domain.SellerAgg;

namespace Shop.Domain.OrderAgg
{
    public class OrderSeller : BaseEntity<int>
    {
        public int OrderId { get; internal set; }
        public int SellerId { get; private set; }
        public OrderSellerStatus Status { get; private set; }
        public int DiscountId { get; private set; }
        public int DiscountPercent { get; private set; }
        public string DiscountTitle { get; private set; }
        public int PostPrice { get; private set; }
        public Order Order { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public Seller Seller { get; private set; }
        public OrderSeller()
        {
            Order = new();
            OrderItems = new();
            Seller = new();
        }

        public OrderSeller(int sellerId)
        {
            SellerId = sellerId;
            Status = OrderSellerStatus.پرداخت_نشده;
            DiscountId = 0;
            DiscountPercent = 0;
            PostPrice = 0;
            OrderItems = new();
            DiscountTitle = "";
        }
        public void AddPostPrice(int price)
        {
            PostPrice = price;
        }
        public void AddDiscount(int discountId, int discountPercent)
        {
            DiscountId = discountId;
            DiscountPercent = discountPercent;
        }
        public void ChangeStatus(OrderSellerStatus status)
        {
            Status = status;
        }
        public int Price
        {
            get
            {
                return OrderItems.Sum(o => o.SumPrice);
            }
        }
        public int PriceAfterOff
        {
            get
            {
                return OrderItems.Sum(o => o.SumPriceAfterOff);
            }
        }
        public int PaymentPrice
        {
            get
            {
                var discountPrice = DiscountPercent * PriceAfterOff / 100;
                return PriceAfterOff - discountPrice;
            }
        }
        public void AddOrderItem(OrderItem item)
        {
            item.OrderSellerId = Id;
            OrderItems.Add(item);
        }
    }
}
