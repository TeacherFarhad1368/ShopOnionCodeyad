using Blogs.Application.Contract.BlogApplication.Command;
using Blogs.Application.Contract.BlogApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UI.Blog;

namespace ShopBoloor.WebApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogQuery _blogQuery;
        private readonly IBlogUiQuery _blogUiQuery;
        private readonly IBlogApplication _blogApplication;
        public BlogController(IBlogQuery blogQuery,IBlogUiQuery blogUiQuery, IBlogApplication blogApplication)
        {
            _blogQuery = blogQuery;
            _blogUiQuery = blogUiQuery;
            _blogApplication = blogApplication;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/GetBestBlogs")]
        public JsonResult GetBestBlogs()
        {
            var model = _blogQuery.GetBestBlogsForMagIndex();
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }
        [Route("/Blogs/{slug?}")]
        public IActionResult Blogs(string slug="",int pageId = 1,string filter = "")
        {
            var model = _blogUiQuery.GetBlogsForUi(slug,pageId,filter);
            return View(model);
        }
        [Route("/Blog/{slug}")]
        public IActionResult Blog(string slug)
        {
            var model = _blogUiQuery.GetSingleBlogForUi(slug);
            if (model == null) return NotFound();
            _blogApplication.VisitBlog(model.Id);
            return View(model);
        }
    }
}
