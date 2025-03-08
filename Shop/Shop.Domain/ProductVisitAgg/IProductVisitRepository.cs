using Shared.Domain;

namespace Shop.Domain.ProductVisitAgg
{
    public interface IProductVisitRepository : IRepository<int, ProductVisit>
    {
        Task<bool> UbsertProductVisitAsymc(ProductVisit command);
    }
}
