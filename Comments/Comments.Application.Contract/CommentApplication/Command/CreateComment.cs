using Shared.Domain.Enum;

namespace Comments.Application.Contract.CommentApplication.Command
{
    public class CreateComment
    {
        public int OwnerId { get; set; }
        public CommentFor For { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public long? ParentId { get; set; }
    }
}
