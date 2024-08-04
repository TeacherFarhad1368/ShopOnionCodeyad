using Shared.Application;
using Shared.Domain;
using Users.Application.Contract.WalletApplication.Command;

namespace Users.Domain.WalletAgg;

public interface IWalletRepository : IRepository<int, Wallet>
{
    Task<OperationResultWithKey> DepositByUserAsync(CreateWalletWithWhy command);
    int GetWalletAmount(int userId);
}
