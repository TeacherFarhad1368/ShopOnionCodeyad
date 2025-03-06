using Shared;

namespace Query.Contract.Admin.Order;

public class OrderAdminPaging : BasePaging
{
    public int UserId { get; set; }
    public string PageTitle { get; set; }
    public OrderAdminStatus Status { get; set; }
    public int OrderId { get; set; }
    public List<OrderForAdminQueryModel> Orders { get; set; }
}
