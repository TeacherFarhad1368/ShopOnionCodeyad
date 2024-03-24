using Emails.Application.Contract.SensEmailApplication.Command;
using Emails.Application.Contract.SensEmailApplication.Query;
using Microsoft.AspNetCore.Mvc;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
	public class SendEmailController : Controller
	{
		private readonly ISendEmailQuery _sendEmailQuery;
		private readonly ISensEmailApplication _sensEmailApplication;

		public SendEmailController(ISendEmailQuery sendEmailQuery, ISensEmailApplication sensEmailApplication)
		{
			_sendEmailQuery = sendEmailQuery;
			_sensEmailApplication = sensEmailApplication;
		}

		public IActionResult Index()
		{
			return View(_sendEmailQuery.GetEmailSendsFoeAdmin());
		}
		public IActionResult Detail(int id)
		{
			return View(_sendEmailQuery.GetSendEmailDetailForAdmin(id));
		}
		public IActionResult Create() => View();
		[HttpPost]
		public IActionResult Create(CreateSendEmail model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _sensEmailApplication.Create(model);
			if(res.Success)
			{
				TempData["ok"] = true;
				return RedirectToAction("Imdex");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
	}
}
