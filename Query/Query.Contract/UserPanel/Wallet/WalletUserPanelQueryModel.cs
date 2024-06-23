using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.Wallet;

public class WalletUserPanelQueryModel
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public int Price { get; set; }
    public WalletType Type { get; set; }
    public string Description { get; set; }
    public string CreationDate { get; set; }
}