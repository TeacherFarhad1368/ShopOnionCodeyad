using Shared.Domain;

namespace Stores.Domain.StoreProductAgg;

public interface IStoreProductRepository : IRepository<int, StoreProduct>
{
    Task<bool> CreateListAsync(List<StoreProduct> storeProducts);
}
