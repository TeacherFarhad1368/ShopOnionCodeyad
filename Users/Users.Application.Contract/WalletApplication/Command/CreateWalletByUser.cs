using Shared.Domain.Enum;

namespace Users.Application.Contract.WalletApplication.Command;

public class CreateWalletWithWhy
{
    public CreateWalletWithWhy(int userId, int price, string description, WalletWhy walletWhy)
    {
        UserId = userId;
        Price = price;
        Description = description;
        WalletWhy = walletWhy;
    }

    public int UserId { get; private set; }
    public int Price { get; private set; }
    public string Description { get; private set; }
    public WalletWhy WalletWhy { get; private set; }
}