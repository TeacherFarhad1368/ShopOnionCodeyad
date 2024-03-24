using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.Admin.Comment;

public interface ICommentAdminQuery
{
	CommentAdminPaging GetForAdmin(int pageId, int take, string filter, int ownerId, 
		CommentFor commentFor, CommentStatus status, long? parentId);
	List<CommentAdminQueryModel> GetAllUnSeenCommentsForAdmin();
}
