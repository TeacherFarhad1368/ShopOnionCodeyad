using Blogs.Application.Contract.BlogApplication.Command;
using Blogs.Application.Contract.BlogApplication.Query;
using Blogs.Application.Contract.BlogCategoryApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogQuery _BlogQuery;
        private readonly IBlogApplication _BlogApplication;
        private readonly IAuthService _authService;
        private readonly IBlogCategoryQuery _blogCategoryQuery;
        public BlogController(IBlogQuery BlogQuery, IBlogApplication BlogApplication, IAuthService
            authService,IBlogCategoryQuery blogCategoryQuery)
        {
            _BlogQuery = BlogQuery;
            _BlogApplication = BlogApplication; 
            _authService = authService;
            _blogCategoryQuery = blogCategoryQuery; 
        }
        public IActionResult Index(int id = 0) => View(_BlogQuery.GetBlogsForAdmin(id));
        public IActionResult Create()
        {
            CreateBlog model = new()
            {
                UserId = _authService.GetLoginUserId(),
                Writer = _authService.GetLoginUserFullName(),
                CategoryId = 0,
                SubCategoryId = 0
            };
            ViewData["Parents"] = _blogCategoryQuery.GetCategoriesForAddBlog(0);
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateBlog model)
        {
            ViewData["Parents"] = _blogCategoryQuery.GetCategoriesForAddBlog(0);
            if (!ModelState.IsValid) return View(model);
            var res = _BlogApplication.Create(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            if (id == 2) return NotFound();
            ViewData["Parents"] = _blogCategoryQuery.GetCategoriesForAddBlog(0);
            var model = _BlogApplication.GetForEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id,EditBlog model)
        {
            ViewData["Parents"] = _blogCategoryQuery.GetCategoriesForAddBlog(0);
            if (!ModelState.IsValid) return View(model);
            var res = _BlogApplication.Edit(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public bool Active(int id) => _BlogApplication.ActivationChange(id);
        public JsonResult GetCategories(int id)
        {
            var model = _blogCategoryQuery.GetCategoriesForAddBlog(id);
            var res = JsonConvert.SerializeObject(model);
            return Json(res);
        }
    }
}
