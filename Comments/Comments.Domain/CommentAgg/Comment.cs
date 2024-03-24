using Shared.Domain;
using Shared.Domain.Enum;
namespace Comments.Domain.CommentAgg
{
    public class Comment : BaseEntityCreate<long>
    {
        public Comment(int userId,int ownerId, CommentFor commentFor, 
            string? fullName, string? email, string text, long? parentId)
        {
            UserId = userId;
            OwnerId = ownerId;
            CommentFor = commentFor;
            FullName = fullName;
            Email = email;
            Text = text;
            ParentId = parentId;
            Status = CommentStatus.خوانده_نشده;
            WhyRejected = null;
        }
        public Comment()
        {
            Childs = new();
            Parent = null;
        }
        public void RejectedComment(string why)
        {
            Status = CommentStatus.رد_شده;
            WhyRejected = why;
        }
        public void AcceptedComment()
        {
            Status = CommentStatus.تایید_شده; 
        }
        public int UserId { get; private set; }
        public int OwnerId { get; private set; }
        public CommentFor CommentFor { get; private set; }
        public CommentStatus Status { get; private set; }
        public string? WhyRejected { get; private set; }
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public string Text { get; private set; }
        public long? ParentId { get; private set; }
        public Comment? Parent { get; private set; }
        public List<Comment> Childs { get; private set; }
    }
}
