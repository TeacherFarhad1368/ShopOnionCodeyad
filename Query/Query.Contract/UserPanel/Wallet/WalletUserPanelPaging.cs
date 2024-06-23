using Shared;

namespace Query.Contract.UserPanel.Wallet;

public class WalletUserPanelPaging : BasePaging
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public int WalletAmount { get; set; }
    public string Filter { get; set; }
    public List<WalletUserPanelQueryModel> Wallets { get; set; }
}
