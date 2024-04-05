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
        public BlogController(IBlogQuery blogQuery,IBlogUiQuery blogUiQuery)
        {
            _blogQuery = blogQuery;
            _blogUiQuery = blogUiQuery; 
        }
        public IActionResult Index()
        {
            return View();
        }
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
    }
}
