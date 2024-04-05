namespace Query.Contract.UI.Blog;

public class BlogCardQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
    public string Slug { get; set; }
    public string ShortDescription { get; set; }
    public string CreationDate { get; set; }
    public string Writer { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string CategorySlug { get; set; }
}