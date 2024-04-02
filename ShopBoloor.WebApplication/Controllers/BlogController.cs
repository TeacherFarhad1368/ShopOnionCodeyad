using Microsoft.AspNetCore.Mvc;

namespace ShopBoloor.WebApplication.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
