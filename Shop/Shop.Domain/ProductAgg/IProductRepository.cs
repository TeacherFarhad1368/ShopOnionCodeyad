using Shared.Domain;

namespace Shop.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<int, Product>
    {
        Task<Product> GetProductByIdAsync(int id);
    }
}
