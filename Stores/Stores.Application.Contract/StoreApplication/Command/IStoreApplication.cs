using Shared.Application;
using Shared.Domain.Enum;

namespace Stores.Application.Contract.StoreApplication.Command;
public interface IStoreApplication
{
    Task<OperationResult> CreateAsync(CreateStore command);
    Task<OperationResult> EditAsync(int id,string des);
}