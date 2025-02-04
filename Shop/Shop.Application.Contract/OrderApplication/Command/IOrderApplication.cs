﻿using Shared.Application;

namespace Shop.Application.Contract.OrderApplication.Command;
public interface IOrderApplication
{
    Task<bool> AddOrderSellerDiscountAsync(int userId, int sellerId, int discountId, string title,
        int percent);
    Task CheckOrderEmpty(int userId);
    Task<bool> DeleteOrderItemAsync(int id, int userId);
    Task<OperationResult> OrderItemMinus(int id, int userId);
    Task<OperationResult> OrderItemPlus(int id, int userId);
    Task<bool> UbsertOpenOrderForUserAsync(int _userId,List<ShopCartViewModel> cart);
}
