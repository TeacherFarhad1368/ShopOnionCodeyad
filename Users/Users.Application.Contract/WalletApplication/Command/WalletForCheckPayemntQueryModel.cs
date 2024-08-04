using Shared.Domain.Enum;

namespace Users.Application.Contract.WalletApplication.Command;

public class WalletForCheckPayemntQueryModel
{
    public WalletForCheckPayemntQueryModel(int id, WalletType type, bool isPay, string description)
    {
        Id = id;
        Type = type;
        IsPay = isPay;
        Description = description;
    }

    public int Id { get; private set; }
    public WalletType Type { get; private set; }
    public bool IsPay { get; private set; }
    public string Description { get; private set; }
}