using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Shop.Application.Contract.ProductFeautreApplication.Command;
using Shop.Application.Contract.ProductFeautreApplication.Query;
using ShopBoloor.WebApplication.Utility;
using Users.Application.Contract.WalletApplication.Command;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Product;

[Area("Admin")]
[PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_محصولات)]
public class ProductFeatureController : Controller
{
    private readonly IProductFeautreApplication _productFeautreApplication;
    private readonly IProductFeautreQuery _productFeautreQuery;

    public ProductFeatureController(IProductFeautreApplication productFeautreApplication, IProductFeautreQuery productFeautreQuery)
    {
        _productFeautreApplication = productFeautreApplication;
        _productFeautreQuery = productFeautreQuery;
    }

    public IActionResult Index(int id)
    {
        var model = _productFeautreQuery.GetProductFeaturesForAdmin(id);
        return View(model);
    }
    public IActionResult Create(int id)
    {
        var model = new CreateProductFeautre()
        {
            ProductId = id
        };
        return PartialView("Create", model);
    }
    [HttpPost]
    public async Task<JsonResult> Create(int id, CreateProductFeautre model)
    {
        OperationResult res = new(false);
        if (id != model.ProductId)
            res = new(false, "لطفا اطلاعات را درست وارد کنید .");
        else
            res = await _productFeautreApplication.CreateAsync(model);
        return new JsonResult(res);
    }
    public async Task<bool> Delete(int id) => await _productFeautreApplication.DeleteAsync(id);
}
