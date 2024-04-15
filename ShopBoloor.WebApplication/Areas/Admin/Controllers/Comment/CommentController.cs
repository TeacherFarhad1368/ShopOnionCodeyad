using Comments.Application.Contract.CommentApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Comment;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Areas.Admin.Controllers.Comment
{
	[Area("Admin")]
	public class CommentController : Controller
	{
		private readonly ICommentAdminQuery _commentAdminQuery;
		private readonly ICommentApplication _commentApplication;

		public CommentController(ICommentAdminQuery commentAdminQuery, ICommentApplication commentApplication)
		{
			_commentAdminQuery = commentAdminQuery;
			_commentApplication = commentApplication;
		}
		[Route("/Admin/Comment/{id}/{type}/{status}")]
		public IActionResult Index(int id,CommentFor type,CommentStatus status,long? parent = null ,int pageId = 1 ,string filter ="",int take = 10)
		{
			return View(_commentAdminQuery.GetForAdmin(pageId,take,filter,id,type,status,parent));
		}
		public IActionResult UnSeen()
		{
			return View(_commentAdminQuery.GetAllUnSeenCommentsForAdmin());
		}
		public bool Accept(long id)
		{
			return _commentApplication.AcceptedComment(id);
		}
		public bool Reject(long id,string why)
		{
			var res = _commentApplication.Reject(new RejectComment()
			{
				Id = id,
				Why = why
			});
			return res.Success;
		}
	}
}
