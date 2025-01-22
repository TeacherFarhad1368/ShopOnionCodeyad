using Discounts.Domain.OrderDiscountAgg;
using Query.Contract.OrderDiscount;
using Shared.Domain.Enum;
using System.Reflection.Metadata.Ecma335;

namespace Query.Services;
internal class OrderDiscountQuery : IOrderDiscountQuery
{
    private readonly IOrderDiscountRepository _orderDiscountRepository;

    public OrderDiscountQuery(IOrderDiscountRepository orderDiscountRepository)
    {
        _orderDiscountRepository = orderDiscountRepository;
    }

    public List<OrderAdminQueryModel> GetAllActivesForAdmin()
    {
        var res = _orderDiscountRepository.GetAllByQuery(o => o.EndDate.Date >= DateTime.Now.Date && o.Type == OrderDiscountType.Order);
        return res.Select(r => new OrderAdminQueryModel(r.Id, r.Percent, r.Title, r.Code, r.Count, r.StartDate, r.EndDate, r.Use, r.CreateDate)).ToList();
    }

    public List<OrderAdminQueryModel> GetAllInActivesForAdmin()
    {
        var res = _orderDiscountRepository.GetAllByQuery(o => o.EndDate.Date < DateTime.Now.Date && o.Type == OrderDiscountType.Order);
        return res.Select(r => new OrderAdminQueryModel(r.Id, r.Percent, r.Title, r.Code, r.Count, r.StartDate, r.EndDate, r.Use, r.CreateDate)).ToList();
    }

    public List<OrderAdminQueryModel> GetAllActivesForSeller(List<int> sellerIds)
    {
        List<OrderDiscount> orderDiscounts = new List<OrderDiscount>();
        foreach (int id in sellerIds)
            orderDiscounts.AddRange(_orderDiscountRepository.GetAllBy(o => o.EndDate.Date >= DateTime.Now.Date && o.Type == OrderDiscountType.OrderSeller && o.ShopId == id));
        
        return orderDiscounts.Select(r => new OrderAdminQueryModel(r.Id, r.Percent, r.Title, r.Code, r.Count, r.StartDate, r.EndDate, r.Use, r.CreateDate)).ToList();
    }

    public List<OrderAdminQueryModel> GetAllInActivesForSeller(List<int> sellerIds)
    {
        List<OrderDiscount> orderDiscounts = new List<OrderDiscount>();
        foreach (int id in sellerIds)
            orderDiscounts.AddRange(_orderDiscountRepository.GetAllBy(o => o.EndDate.Date < DateTime.Now.Date && o.Type == OrderDiscountType.OrderSeller && o.ShopId == id));
        
        return orderDiscounts.Select(r => new OrderAdminQueryModel(r.Id, r.Percent, r.Title, r.Code, r.Count, r.StartDate, r.EndDate, r.Use, r.CreateDate)).ToList();
    }
}
