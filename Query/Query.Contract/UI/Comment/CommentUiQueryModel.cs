namespace Query.Contract.UI.Comment;

public class CommentUiQueryModel
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string CreationDate { get; set; }
    public string Text { get; set; }
    public string Avatar { get; set; }
    public List<CommentUiQueryModel> Childs { get; set; }
}