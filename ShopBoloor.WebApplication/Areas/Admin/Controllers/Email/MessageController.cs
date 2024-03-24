using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.MessageUser;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
	public class MessageController : Controller
	{
		private readonly IMessageUserAdminQuery _messageUserAdminQuery;
		private readonly IMessageUserApplication _messageUserApplication;

		public MessageController(IMessageUserAdminQuery messageUserAdminQuery, IMessageUserApplication messageUserApplication)
		{
			_messageUserAdminQuery = messageUserAdminQuery;
			_messageUserApplication = messageUserApplication;
		}

		public IActionResult Index(int pageId = 1,int take = 10,string filter = "" ,MessageStatus status = MessageStatus.همه)
		{
			return View(_messageUserAdminQuery.GetMessagesForAdmin(pageId,take,filter,status));
		}
		public IActionResult Detail(int id)
		{
			return View(_messageUserAdminQuery.GetMessageDetailForAdmin(id));
		}
		public bool ChangeStatus(int id, string answer , MessageStatus status)
		{
			switch (status)
			{
				case MessageStatus.پاسخ_داده_شد_sms:
					if (!string.IsNullOrEmpty(answer))
					{
						// send sms
					}
					var res = _messageUserApplication.AnsweredBySMS(id,answer);
					return res.Success;
				case MessageStatus.پاسخ_داده_شد_email:
					if (!string.IsNullOrEmpty(answer))
					{
						// send email
					}
					var result = _messageUserApplication.AnsweredByEmail(id, answer);
					return result.Success;
				default:
					return _messageUserApplication.AnswerByCall(id);
			}
		}
	}
}
