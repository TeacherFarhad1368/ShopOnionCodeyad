using Shared.Domain.Enum;
using Shared.Infrastructure;
using Users.Domain.WalletAgg;

namespace Users.Infrastructure.Service;

internal class WalletRepository : Repository<int, Wallet>, IWalletRepository
{
    private readonly UserContext _context;
    public WalletRepository(UserContext context) : base(context)
    {
        _context = context;
    }

    public int GetWalletAmount(int userId)
    {
        int deposits = GetAllByQuery(w => w.IsPay && w.Type == WalletType.واریز).Sum(w => w.Price);
        int withdraws = GetAllByQuery(w => w.IsPay && w.Type == WalletType.برداشت).Sum(w => w.Price);
        return deposits - withdraws;
    }
}