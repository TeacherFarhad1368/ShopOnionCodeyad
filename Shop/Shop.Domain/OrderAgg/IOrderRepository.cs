using Shared.Domain;

namespace Shop.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<int, Order> { }
}
