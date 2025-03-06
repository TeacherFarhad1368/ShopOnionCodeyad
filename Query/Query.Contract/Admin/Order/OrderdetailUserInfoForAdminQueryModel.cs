namespace Query.Contract.Admin.Order;

public class OrderdetailUserInfoForAdminQueryModel
{
    public int UserId { get; set; }
    public string Mobile { get; set; }
    public string? Email { get; set; }
    public string FullName { get; set; }
}