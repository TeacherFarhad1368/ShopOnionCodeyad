using Shop.Application.Contract.ProductVisitApplication.Command;
using Shop.Domain.ProductVisitAgg;

namespace Shop.Application.Services;
internal class ProductVisitApplication : IProductVisitApplication
{
    private readonly IProductVisitRepository _productVisitRepository;
    public ProductVisitApplication(IProductVisitRepository productVisitRepository)
    {
        _productVisitRepository = productVisitRepository;
    }
    public async Task<bool> CreateAsync(CreateProductVisit command)=>
        await _productVisitRepository.UbsertProductVisitAsymc(new ProductVisit(command.ProductId, command.UserId, 1));
}
