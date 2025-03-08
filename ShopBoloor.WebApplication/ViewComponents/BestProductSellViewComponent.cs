using Microsoft.AspNetCore.Mvc;
using Query.Contract.UI.Product;

namespace ShopBoloor.WebApplication.ViewComponents;

public class BestProductSellViewComponent : ViewComponent
{
    private readonly IProductUiQuery _productUiQuery;
    public BestProductSellViewComponent(IProductUiQuery productUiQuery)
    {
        _productUiQuery = productUiQuery;
    }
    public IViewComponentResult Invoke()
    {
        var model = _productUiQuery.GetBestPeoductSellForIndex();
        return View(model);
    }
}
public class NewProductViewComponent : ViewComponent
{
    private readonly IProductUiQuery _productUiQuery;
    public NewProductViewComponent(IProductUiQuery productUiQuery)
    {
        _productUiQuery = productUiQuery;
    }
    public IViewComponentResult Invoke()
    {
        var model = _productUiQuery.GetNewPeoductForIndex();
        return View(model);
    }
}