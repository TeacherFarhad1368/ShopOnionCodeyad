using Comments.Application.Contract.CommentApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UI.Comment;
using Shared.Application.Services.Auth;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentUiQuery _commentUiQuery;
        private readonly ICommentApplication _commentApplication;
        private readonly IAuthService _authService;
        public CommentController(ICommentUiQuery commentUiQuery, ICommentApplication commentApplication, IAuthService authService)
        {
            _commentUiQuery = commentUiQuery;
            _authService = authService;
            _commentApplication = commentApplication;
        }
        [Route("/Comments/{ownerId}/{commentFor}")]
        public IActionResult Comments(int ownerId, CommentFor commentFor,int pageId = 1)
        {
            var model = _commentUiQuery.GetCommentsForUi(ownerId, commentFor, pageId);
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }
        [HttpPost]
        public IActionResult Create(int ownerId,CommentFor commentFor,long? parentId,
            string? email,string fullName,string text)
        {
            var userId = _authService.GetLoginUserId();
            CreateComment createComment = new CreateComment()
            {
            Email = email,
            For = CommentFor.مقاله,
            FullName = fullName,
            OwnerId = ownerId,
            ParentId = parentId,
            Text = text,
            UserId = userId
            };
            var res = _commentApplication.Create(createComment);
            if (res.Success)
                TempData["SuccessCreateComment"] = true;
            else
                TempData["FaildCreateComment"] = true;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
