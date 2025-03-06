using Shared;

namespace Query.Contract.UserPanel.Order;

public class OrderUserPanelPaging : BasePaging
{
    public List<OrderForUserPanelQueryModel> Orders { get; set; }
}