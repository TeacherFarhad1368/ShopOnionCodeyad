namespace Blogs.Application.Contract.BlogApplication.Query;

public class BestBlogQueryModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Writer { get; set; }
    public string CreationDate { get; set; }
    public string CategoryTitle { get; set; }
    public string CategorySlug { get; set; }
    public string ImageName { get; set; }
    public string ImageName400 { get; set; }
    public string ImageAlt { get; set; }
    public int Visit { get; set; }
    public int Category { get; set; }
    public int SubCategory { get; set; }
}