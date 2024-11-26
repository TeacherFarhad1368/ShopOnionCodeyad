using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.ProductCategoryApplication.Query;
using Shop.Application.Contract.ProductSellApplication.Command;
namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers.Seller;

[Area("UserPanel")]
[Authorize]
public class ProductController : Controller
{
    private readonly ISellerUserPanelQuery _sellerUserPanelQuery;
    private readonly IProductSellApplication _productSellApplication;
    private readonly IProductCategoryQuery _productCategoryQuery;
    private readonly IAuthService _authService;
    private int _userId;

    public ProductController(ISellerUserPanelQuery sellerUserPanelQuery, IAuthService authService
        , IProductSellApplication productSellApplication, IProductCategoryQuery productCategoryQuery)
    {
        _sellerUserPanelQuery = sellerUserPanelQuery;
        _productSellApplication = productSellApplication;
        _authService = authService;
        _productCategoryQuery = productCategoryQuery;
    }
    public IActionResult Index(int id,int pageId = 1,string filter = "")
    {
        _userId = _authService.GetLoginUserId();
        var model = _sellerUserPanelQuery.GetSellerProductsForUserPanel(pageId,filter,id,_userId);
        if (model == null) return NotFound();
        return View(model); 
    }
    public IActionResult Create(int id)
    {
        if(id == 0) return Redirect("/UserPanel/Seller/Index");   
        CreateProductSell model = new()
        {
            SellerId = id
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(int id,CreateProductSell model)
    {
        if (ModelState.IsValid == false) return View(model);
        if (model.SellerId != id) return NotFound();
        var res = await _productSellApplication.CreateAsync(model);
        if (res.Success)
        {
            TempData["ok"] = true;
            return Redirect($"/UserPanel/Product/Index/{id}");
        }
        ModelState.AddModelError(res.ModelName, res.Message);
        return View(model);
    }
    [HttpPost]
    public JsonResult Categories(int id = 0)
    {
        var res = _productCategoryQuery.GetCategoryForAddProductSells(id);
        return Json(JsonConvert.SerializeObject(res));
    }
}
