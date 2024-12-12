using Shared.Application;
using Stores.Application.Contract.StoreApplication.Command;
using Stores.Domain.StoreAgg;
using Stores.Domain.StoreProductAgg;

namespace Stores.Application.Services;
internal class StoreApplication : IStoreApplication
{
    private readonly IStoreRepository _storeRepository;
    private readonly IStoreProductRepository _storeProductRepository;
    public StoreApplication(IStoreRepository storeRepository, IStoreProductRepository storeProductRepository)
    {
        _storeRepository = storeRepository;
        _storeProductRepository = storeProductRepository;   
    }

    public async Task<OperationResult> CreateAsync(int _userId, CreateStore command)
    {
        Store store = new(_userId, command.SellerId, command.Description);
        int id = await _storeRepository.CreateReturnKey(store);
        if (id < 1) return new(false);
        else
        {
            List<StoreProduct> storeProducts = command.Products
                .Select(s => new StoreProduct(id, s.ProductSellId, s.Type, s.Count)).ToList();
            if(await _storeProductRepository.CreateListAsync(storeProducts))
            {
                return new OperationResult(true);   
            }
            else
            {
                await _storeRepository.DeleteAsync(store);
                return new(false);
            }
        }
    }

    public Task<OperationResult> EditAsync(int id, string des)
    {
        throw new NotImplementedException();
    }
}
