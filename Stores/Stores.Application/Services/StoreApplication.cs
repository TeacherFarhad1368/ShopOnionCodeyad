using Shared.Application;
using Stores.Application.Contract.StoreApplication.Command;
using Stores.Domain.StoreAgg;

namespace Stores.Application.Services;
internal class StoreApplication : IStoreApplication
{
    private readonly IStoreRepository _storeRepository;

    public StoreApplication(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public Task<OperationResult> CreateAsync(CreateStore command)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> EditAsync(int id, string des)
    {
        throw new NotImplementedException();
    }
}
