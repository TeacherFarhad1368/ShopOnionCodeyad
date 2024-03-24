using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostApplication;
using PostModule.Application.Contract.PostQuery;
using System.Diagnostics.Contracts;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Post
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostQuery _postQuery;
        private readonly IPostApplication _postApplication;

        public PostController(IPostQuery postQuery, IPostApplication postApplication)
        {
            _postQuery = postQuery;
            _postApplication = postApplication;
        }

        public IActionResult Index()
        {
            return View(_postQuery.GetAllPostsForAdmin());
        }
        public IActionResult Create() => View();
        [HttpPost] 
        public IActionResult Create(CreatePost model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _postApplication.Create(model);
            if(res.Success)
            {
                TempData["ok"] = true;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public IActionResult Edit(int id) => View(_postApplication.GetForEdit(id));
        [HttpPost]
        public IActionResult Edit(int id,EditPost model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = _postApplication.Edit(model);
            if (res.Success)
            {
                TempData["ok"] = true;
                return Redirect($"/Admin/Post/Edit/{id}");
            }
            ModelState.AddModelError(res.ModelName, res.Message);
            return View(model);
        }
        public bool Active(int id) => _postApplication.ActivationChange(id);
        public bool Inside(int id) => _postApplication.InsideCityChange(id);
        public bool Outside(int id) => _postApplication.OutSideCityChange(id);
    }
}
