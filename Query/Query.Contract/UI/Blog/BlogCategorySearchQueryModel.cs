namespace Query.Contract.UI.Blog;

public class BlogCategorySearchQueryModel
{
    public string Title  { get; set; }
    public string Slug { get; set; }
    public int BlogCount { get; set; }
    public int Id { get; set; }
    public List<BlogCategorySearchQueryModel> Childs { get; set; }
}
