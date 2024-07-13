using Shared.Domain.Enum;

namespace Query.Contract.Admin.Wallet;

public class UserWalletAdminQueryModel
{
    public int Id { get; set; }
    public int Price { get; set; }
    public WalletType Type { get; set; }
    public WalletWhy WalletWhy { get; set; }
    public bool IsPay { get; set; }
    public string Description { get; set; }
    public string CreationDate { get; set; }
}