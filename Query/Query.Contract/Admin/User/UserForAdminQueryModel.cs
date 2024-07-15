namespace Query.Contract.Admin.User;

public class UserForAdminQueryModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public bool Delete { get; set; }
    public string Creationdate { get; set; }
    public string AvatarAddress { get; set; }
    public int WalletAmount { get; set; }
    public int TransactionSuccessCount { get; set; }
    public int TransactionSuccessSum { get; set; }
    public int OrderCount { get; set; }
    public int OrderSum { get; set; }
}