using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.Admin;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminQuery _adminQuery;

        public HomeController(IAdminQuery adminQuery)
        {
            _adminQuery = adminQuery;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetChartData(string year = "0")
        {
           var model = _adminQuery.GetTransactionChartData(year);
           var json = JsonConvert.SerializeObject(model);
           return Json(json);
        }
    }
}
