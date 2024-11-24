using Shared.Domain;
namespace Shop.Domain.SellerAgg;

public interface ISellerRepository : IRepository<int, Seller>
{
    Seller? GetSellerForUserPanel(int id, int userId);
}
