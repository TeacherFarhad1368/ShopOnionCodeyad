﻿using Shared.Domain;

namespace Discounts.Domain.OrderDiscountAgg
{
    public interface IOrderDiscountRepository : IRepository<int, OrderDiscount>
    {
        Task<OrderDiscount> GetByCodeAsync(string code);
    }
}
