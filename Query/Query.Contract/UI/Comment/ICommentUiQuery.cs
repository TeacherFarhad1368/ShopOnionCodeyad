using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UI.Comment;

public interface ICommentUiQuery
{
    CommentUiPaging GetCommentsForUi(int ownerId, CommentFor commentFor,int pageId);
}
