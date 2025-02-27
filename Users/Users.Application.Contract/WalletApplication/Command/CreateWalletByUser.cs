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
    public CreateWalletWithWhy()
    {
        
    }
    public int UserId { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public WalletWhy WalletWhy { get; set; }
}