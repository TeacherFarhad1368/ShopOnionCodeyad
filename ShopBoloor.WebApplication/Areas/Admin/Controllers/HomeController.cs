using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.Admin;
using Query.Contract.Admin.MessageUser;
using Shared.Domain.Enum;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(UserPermission.پنل_ادمین)]
    public class HomeController : Controller
    {
        private readonly IAdminQuery _adminQuery;
        private readonly IMessageUserAdminQuery _messageUserAdminQuery;

        public HomeController(IAdminQuery adminQuery, IMessageUserAdminQuery messageUserAdminQuery)
        {
            _adminQuery = adminQuery;
            _messageUserAdminQuery = messageUserAdminQuery;
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
        [HttpPost]
        public JsonResult GetNotification(string year = "0")
        {
            var model = _adminQuery.GetNotificationForAdmin();
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }

        [HttpPost]
        public JsonResult GetMessageNotifications()
        {
            var model = _messageUserAdminQuery.GetUnSeenUserMessagesFotNotifes();
            var jsom = JsonConvert.SerializeObject(model);
            return Json(jsom);
        }
    }
}
