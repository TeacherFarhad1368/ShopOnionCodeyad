using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contract.ProductCategoryApplication.Command;
using Shop.Application.Contract.ProductCategoryApplication.Query;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Product
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryQuery _ProductCategoryQuery;
        private readonly IProductCategoryApplication _ProductCategoryApplication;
        public ProductCategoryController(IProductCategoryQuery ProductCategoryQuery, IProductCategoryApplication ProductCategoryApplication)
        {
            _ProductCategoryQuery = ProductCategoryQuery;
            _ProductCategoryApplication = ProductCategoryApplication;
        }
        public async Task<IActionResult> Index(int id = 0)
        {

            if (id > 0 && await _ProductCategoryQuery.CheckCategoryHaveParent(id)) return NotFound();
            return View(_ProductCategoryQuery.GetCategoriesForAdmin(id));
        }
        public async Task<IActionResult> Create(int id = 0)
        {
            if (id > 0 && await _ProductCategoryQuery.CheckCategoryHaveParent(id)) return NotFound();
            return View(new CreateProductCategory()
            {
                Parent = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateProductCategory model)
        {
            if (id != model.Parent) return NotFound();
            if (!ModelState.IsValid) return View(model);
            var res = await _ProductCategoryApplication.CreateAsync(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/ProductCategory/Index/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _ProductCategoryApplication.GetForEditAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductCategory model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = await _ProductCategoryApplication.EditAsync(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/ProductCategory/Edit/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public async Task<bool> Active(int id) => await _ProductCategoryApplication.ActivationChange(id);
    }
}
