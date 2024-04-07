using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UI.Comment;
using Shared.Domain.Enum;

namespace ShopBoloor.WebApplication.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentUiQuery _commentUiQuery;

        public CommentController(ICommentUiQuery commentUiQuery)
        {
            _commentUiQuery = commentUiQuery;
        }
        [Route("/Comments/{ownerId}/{commentFor}")]
        public IActionResult Comments(int ownerId, CommentFor commentFor,int pageId = 1)
        {
            var model = _commentUiQuery.GetCommentsForUi(ownerId, commentFor, pageId);
            var json = JsonConvert.SerializeObject(model);
            return Json(json);
        }
    }
}
