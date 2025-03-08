namespace Shop.Application.Contract.ProductVisitApplication.Command;
public interface IProductVisitApplication
{
    Task<bool> CreateAsync(CreateProductVisit command);
}
