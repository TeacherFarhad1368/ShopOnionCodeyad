﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UserPanel.Seller;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.ProductApplication;
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
    private readonly IProductQuery _productQuery;
    private readonly IAuthService _authService;
    private int _userId;

    public ProductController(ISellerUserPanelQuery sellerUserPanelQuery, IAuthService authService
        , IProductSellApplication productSellApplication, IProductCategoryQuery productCategoryQuery,
        IProductQuery productQuery)
    {
        _sellerUserPanelQuery = sellerUserPanelQuery;
        _productSellApplication = productSellApplication;
        _authService = authService;
        _productCategoryQuery = productCategoryQuery;
        _productQuery = productQuery;       
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
        _userId = _authService.GetLoginUserId();
        bool ok = _sellerUserPanelQuery.IsSellerForUser(id,_userId);
        if (!ok) return NotFound();
        if (id == 0) return Redirect("/UserPanel/Seller/Index");   
        CreateProductSell model = new()
        {
            SellerId = id
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(int id,CreateProductSell model)
    {
        _userId = _authService.GetLoginUserId();
        if (ModelState.IsValid == false) return View(model);
        if (model.SellerId != id) return NotFound();
        bool ok = _sellerUserPanelQuery.IsSellerForUser(id, _userId);
        if (!ok) return NotFound();
        var res = await _productSellApplication.CreateAsync(model);
        if (res.Success)
        {
            TempData["ok"] = true;
            return Redirect($"/UserPanel/Product/Index/{id}");
        }
        ModelState.AddModelError(res.ModelName, res.Message);
        return View(model);
    }
    public async Task<IActionResult> Edit(int id)
    {
        _userId = _authService.GetLoginUserId();
        bool ok = await _sellerUserPanelQuery.IsProductSellForUser(_userId, id);
        if(ok == false) return NotFound();
        var model = await _productSellApplication.GetForEditAsync(id);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id,EditProductSell model)
    {
        _userId = _authService.GetLoginUserId();
        bool ok = await _sellerUserPanelQuery.IsProductSellForUser(_userId, id);
        if (id != model.Id || ok == false) return NotFound();
        if(!ModelState.IsValid) return View(model);
        var res = await _productSellApplication.EditAsync(model);
        if(res.Success)
        {
            TempData["ok"] = true;
            return Redirect($"/UserPanel/Product/Index/{model.SellerId}");
        }
        ModelState.AddModelError(res.ModelName , res.Message);
        return View(model);
    }
    public async Task<bool> Active(int id) => await _productSellApplication.ActivationChangeAsync(id);
    [HttpPost]
    public JsonResult Categories(int id = 0)
    {
        var res = _productCategoryQuery.GetCategoryForAddProductSells(id);
        return Json(JsonConvert.SerializeObject(res));
    }
    [HttpPost]
    public JsonResult GetProducts(int id)
    {
        if(id == 0)
        {
            List<ProductForAddProductSellQueryModel> model = new();
            return Json(JsonConvert.SerializeObject(model));
        }
        else
        {
            var res = _productQuery.GetProductsForAddProductSells(id);
            return Json(JsonConvert.SerializeObject(res));
        }
       
    }
}
