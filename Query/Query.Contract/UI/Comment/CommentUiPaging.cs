using Shared;
using Shared.Domain.Enum;

namespace Query.Contract.UI.Comment;

public class CommentUiPaging : BasePaging
{
    public List<CommentUiQueryModel> Comments { get; set; }
    public int OwnerId { get; set; }
    public CommentFor CommentFor { get; set; }
}
