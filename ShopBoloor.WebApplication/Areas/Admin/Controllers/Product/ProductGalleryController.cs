using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Shop.Application.Contract.ProductGalleryApplication.Command;
using Shop.Application.Contract.ProductGalleryApplication.Query;
using ShopBoloor.WebApplication.Utility;
namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Product;
[Area("Admin")]
[PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_محصولات)]
public class ProductGalleryController : Controller
{
    private readonly IProductGalleryApplication _productGalleryApplication;
    private readonly IProductGalleryQuery _productGalleryQuery;

    public ProductGalleryController(IProductGalleryApplication productGalleryApplication,
        IProductGalleryQuery productGalleryQuery)
    {
        _productGalleryApplication = productGalleryApplication;
        _productGalleryQuery = productGalleryQuery;
    }

    public IActionResult Index(int id)
    {
        var model = _productGalleryQuery.GetProductGalleriesForAdmin(id);
        return View(model);
    }
    public IActionResult Create(int id)
    {
        var model = new CreateProductGallery()
        {
            ProductId = id
        };
        return PartialView("Create", model);
    }
    [HttpPost]
    public async Task<JsonResult> Create(int id, CreateProductGallery model)
    {
        OperationResult res = new(false);
        if (id != model.ProductId)
            res = new(false, "لطفا اطلاعات را درست وارد کنید .");
        else
            res = await _productGalleryApplication.CreateAsync(model);
        return new JsonResult(res);
    }
    public async Task<bool> Delete(int id) => await _productGalleryApplication.DeleteAsync(id);
}