using Blogs.Application.Contract.BlogApplication.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ShopBoloor.WebApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogQuery _blogQuery;
        public BlogController(IBlogQuery blogQuery)
        {
            _blogQuery = blogQuery;
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
    }
}
