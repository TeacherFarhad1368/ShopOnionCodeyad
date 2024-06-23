using Shared.Domain.Enum;
using Users.Application.Contract.WalletApplication.Query;
using Users.Domain.WalletAgg;

namespace Users.Query.Services;

internal class WalletQuery : IWalletQuery
{
    private readonly IWalletRepository _walletRepository;

    public WalletQuery(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public int GetWalletSum(int userId)
    {
        return _walletRepository.GetWalletAmount(userId);
    }
}