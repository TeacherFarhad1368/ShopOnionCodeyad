namespace Blogs.Application.Contract.BlogApplication.Query;

public class BestBlogForMagIndexQueryModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Writer { get; set; }
    public string CreationDate { get; set; }
    public string ImageName { get; set; }
    public string ShortDescription { get; set; }
    public string ImageAlt { get; set; }
}