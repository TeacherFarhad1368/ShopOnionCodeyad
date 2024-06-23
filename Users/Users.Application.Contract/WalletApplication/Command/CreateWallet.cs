namespace Users.Application.Contract.WalletApplication.Command;

public class CreateWallet
{
    public CreateWallet(int userId, int price, string description)
    {
        UserId = userId;
        Price = price;
        Description = description;
    }

    public int UserId { get; private set; }
    public int Price { get; private set; }
    public string Description { get; private set; }
}