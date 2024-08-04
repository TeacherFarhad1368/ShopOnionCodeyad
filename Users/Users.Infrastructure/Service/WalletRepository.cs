using Shared.Application;
using Shared.Domain.Enum;
using Shared.Infrastructure;
using Users.Application.Contract.WalletApplication.Command;
using Users.Domain.WalletAgg;

namespace Users.Infrastructure.Service;

internal class WalletRepository : Repository<int, Wallet>, IWalletRepository
{
    private readonly UserContext _context;
    public WalletRepository(UserContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OperationResultWithKey> DepositByUserAsync(CreateWalletWithWhy command)
    {
        var wallet = Wallet.DepositByUser(command.UserId, command.Price, command.Description, command.WalletWhy);
        if (await CreateAsync(wallet))
        {
            return new(true, "", "", wallet.Id);
        }

        return new(false, ValidationMessages.SystemErrorMessage);
    }

    public int GetWalletAmount(int userId)
    {
        int deposits = GetAllByQuery(w => w.UserId == userId && w.IsPay && w.Type == WalletType.واریز).Sum(w => w.Price);
        int withdraws = GetAllByQuery(w =>w.UserId == userId && w.IsPay && w.Type == WalletType.برداشت).Sum(w => w.Price);
        return deposits - withdraws;
    }
}