using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Blogs.Application.Contract.BlogCategoryApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryQuery _BlogCategoryQuery;
        private readonly IBlogCategoryApplication _BlogCategoryApplication;
        public BlogCategoryController(IBlogCategoryQuery BlogCategoryQuery, IBlogCategoryApplication BlogCategoryApplication)
        {
            _BlogCategoryQuery = BlogCategoryQuery;
            _BlogCategoryApplication = BlogCategoryApplication; 
        }
        public IActionResult Index(int id = 0)
        {

            if (id > 0 && _BlogCategoryQuery.CheckCategoryHaveParent(id)) return NotFound();
            return View(_BlogCategoryQuery.GetCategoriesForAdmin(id));
        }
        public IActionResult Create(int id = 0) 
        {
            if (id > 0 && _BlogCategoryQuery.CheckCategoryHaveParent(id)) return NotFound();
           return View(new CreateBlogCategory()
            {
                Parent = id
            });
        } 
        [HttpPost]
        public IActionResult Create(int id,CreateBlogCategory model)
        {
            if (id != model.Parent) return NotFound();
            if (!ModelState.IsValid) return View(model);
            var res = _BlogCategoryApplication.Create(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/BlogCategory/Index/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = _BlogCategoryApplication.GetForEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id,EditBlogCategory model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _BlogCategoryApplication.Edit(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/BlogCategory/Index/{model.Parent}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public bool Active(int id) => _BlogCategoryApplication.ActivationChange(id);
    }
}
