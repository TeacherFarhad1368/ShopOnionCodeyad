using Shared.Domain;

namespace Comments.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long, Comment> { }
}
