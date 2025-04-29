using Emails.Application.Contract.EmailUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Email;
using ShopBoloor.WebApplication.Utility;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
    [PermissionChecker(Shared.Domain.Enum.UserPermission.مدیریت_خبرنامه)]
    public class EmailUserController : Controller
	{
		private readonly IEmailAdminQuery _emailAdminQuery;
		private readonly IEmailUserApplication _emailUserApplication;

		public EmailUserController(IEmailAdminQuery emailAdminQuery, IEmailUserApplication emailUserApplication)
		{
			_emailAdminQuery = emailAdminQuery;
			_emailUserApplication = emailUserApplication;
		}

		public IActionResult Index(int pageId = 1, int take = 10 , string filter ="")
		{
			return View(_emailAdminQuery.GetEmailUsersForAdmin(pageId,take,filter));
		}
		public bool Active(int id) => _emailUserApplication.ActivationChange(id);
	}
}
