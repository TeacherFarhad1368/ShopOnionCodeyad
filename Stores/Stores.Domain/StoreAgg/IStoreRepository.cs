using Shared.Domain;

namespace Stores.Domain.StoreAgg;

public interface IStoreRepository : IRepository<int, Store>
{
    Task<int> CreateReturnKey(Store store);
}
