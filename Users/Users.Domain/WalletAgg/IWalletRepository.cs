using Shared.Domain;

namespace Users.Domain.WalletAgg;

public interface IWalletRepository : IRepository<int, Wallet>
{
    int GetWalletAmount(int userId);
}
