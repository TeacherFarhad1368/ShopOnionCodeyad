using Query.Contract.UserPanel.Order;

namespace Query.Contract.Admin.Order;
public interface IOrderAdminQuery
{
    OrderAdminPaging GetOrdersForAdmin(int pageId, int take, int OrderId, int userId, OrderAdminStatus Status);
    OrderDetailForAdminQueryModel GetOrderDetailForAdmin(int id);
}