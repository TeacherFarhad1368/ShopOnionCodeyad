﻿using Shared.Domain;
using Shared.Domain.Enum;

namespace Shop.Domain.OrderAgg
{
    public class Order : BaseEntityCreateUpdate<int>
    {
        public int UserId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public OrderPayment OrderPayment { get; private set; }
        public int OrderAddressId { get; private set; }
        public int PostId { get; private set; }
        public string? PostTitle { get; private set; }
        public int DiscountId { get; private set; }
        public int DiscountPercent { get; private set; }
        public List<OrderSeller> OrderSellers { get; private set; }
        public int Price
        {
            get
            {
                return OrderSellers.Sum(o => o.Price);
            }
        }
        public int PriceAfterOff
        {
            get
            {
                return OrderSellers.Sum(o => o.PriceAfterOff);
            }
        }
        public int PaymentPriceSeller
        {
            get
            {
                return OrderSellers.Sum(o => o.PaymentPrice);
            }
        }
        public int PostPrice
        {
            get
            {
                return OrderSellers.Sum(o => o.PostPrice);
            }
        }
        public int PaymentPrice
        {
            get
            {
                var discountPrice = DiscountPercent * PriceAfterOff / 100;


                return PaymentPriceSeller - discountPrice - PostPrice;
            }
        }
        public Order()
        {
            OrderSellers = new();
        }
        public Order(int userId)
        {
            UserId = userId;
            OrderStatus = OrderStatus.پرداخت_نشده;
            OrderPayment = OrderPayment.پرداخت_از_درگاه;
            OrderAddressId = 0;
            PostId = 0;
            PostTitle = "";
            DiscountId = 0;
            DiscountPercent = 0;
        }
        public void ChamgeStatus(OrderStatus status)
        {
            OrderStatus = status;
            UpdateEntity();
        }
        public void ChangePayment(OrderPayment payment)
        {
            OrderPayment = payment;
        }
        public void AddAddress(int addressId)
        {
            OrderAddressId = addressId;
            UpdateEntity();
        }
        public void ChangeShipping(int postId, string postTitle)
        {
            PostId = postId;
            PostTitle = postTitle;
            UpdateEntity();
        }
        public void AddDiscount(int discountId, int percent)
        {
            DiscountId = discountId;
            DiscountPercent = percent;
            UpdateEntity();
        }
    }
}