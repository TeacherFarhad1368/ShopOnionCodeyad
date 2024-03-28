using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostApplication;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Application.Contract.PostQuery;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Post
{
    [Area("Admin")]
    public class PostPriceController : Controller
    {
        private readonly IPostQuery _postQuery;
        private readonly IPostPriceApplication _postPriceApplication;

        public PostPriceController(IPostQuery postQuery, IPostPriceApplication postPriceApplication)
        {
            _postQuery = postQuery;
            _postPriceApplication = postPriceApplication;
        }

        public IActionResult Index(int id)
        {
            return View(_postQuery.GetPostDetails(id));
        }
        public IActionResult Create(int id) => View(new CreatePostPrice { PostId = id });
        [HttpPost]
        public IActionResult Create(int id,CreatePostPrice model)
        {
            if (id != model.PostId) return NotFound();
            if (!ModelState.IsValid) return View(model);
            var res = _postPriceApplication.Create(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/PostPrice/Index/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public IActionResult Edit(int id) => View(_postPriceApplication.GetForEdit(id));
        [HttpPost]
        public IActionResult Edit(int id,EditPostPrice model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _postPriceApplication.Edit(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/PostPrice/Edit/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
    }
}
