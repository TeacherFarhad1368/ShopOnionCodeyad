namespace Comments.Application.Contract.CommentApplication.Command
{
    public class RejectComment
    {
        public long Id { get; set; }
        public string Why { get; set; }
    }
}
