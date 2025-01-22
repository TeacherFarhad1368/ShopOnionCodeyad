using Shared.Application;
using Shared.Domain.Enum;

namespace Query.Contract.OrderDiscount;
public interface IOrderDiscountQuery
{
    List<OrderAdminQueryModel> GetAllActivesForAdmin();
    List<OrderAdminQueryModel> GetAllActivesForSeller(List<int> sellerIds);
    List<OrderAdminQueryModel> GetAllInActivesForAdmin();
    List<OrderAdminQueryModel> GetAllInActivesForSeller(List<int> sellerIds);
}
public class OrderAdminQueryModel
{
    public OrderAdminQueryModel(int id, int percent, string title, string code, int count,
         DateTime startDate, DateTime endDate, int use, DateTime creationDate)
    {
        Id = id;
        Percent = percent;
        Title = title;
        Code = code;
        Count = count;

        StartDate = startDate.ToPersainDate();
        EndDate = endDate.ToPersainDate();
        Use = use;
        CreationDate = creationDate.ToPersainDate();
    }

    public int Id { get; private set; }
    public int Percent { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public int Count { get; private set; }

    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public int Use { get; private set; }
    public string CreationDate { get; private set; }
}