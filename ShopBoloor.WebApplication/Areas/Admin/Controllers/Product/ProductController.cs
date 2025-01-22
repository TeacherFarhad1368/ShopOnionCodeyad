using Shop.Application.Contract.ProductApplication.Command;
using Shop.Application.Contract.ProductCategoryApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.ProductApplication;
using Query.Contract.Admin.Product;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Product
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductAdminQuery _productAdminQuery;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductCategoryQuery _ProductCategoryQuery;
        public ProductController(IProductAdminQuery productAdminQuery, IProductApplication ProductApplication, 
            IProductCategoryQuery ProductCategoryQuery)
        {
            _productAdminQuery = productAdminQuery;
            _ProductApplication = ProductApplication;
            _ProductCategoryQuery = ProductCategoryQuery;
        }
        public IActionResult Index(int pageId = 1,int take = 10,
            int categoryId = 0,string filter = "",ProductAdminOrderBy orderBy = ProductAdminOrderBy.تاریخ_ثبت_از_آخر)
            => View(_productAdminQuery.GetProductsForAdmin(pageId,take,categoryId,filter,orderBy));
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _ProductCategoryQuery.GetCategoriesForAddProduct();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct model)
        {
            ViewData["Categories"] = await _ProductCategoryQuery.GetCategoriesForAddProduct();
            if (!ModelState.IsValid) return View(model);
            var res = await _ProductApplication.CreateAsync(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = await _ProductCategoryQuery.GetCategoriesForAddProduct();
            var model = await _ProductApplication.GetForEditAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProduct model)
        {
            ViewData["Categories"] = await _ProductCategoryQuery.GetCategoriesForAddProduct();
            if (!ModelState.IsValid) return View(model);
            var res = await _ProductApplication.EditAsync(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public async Task<bool> Active(int id) => await _ProductApplication.ActivationChange(id);
    }
}
