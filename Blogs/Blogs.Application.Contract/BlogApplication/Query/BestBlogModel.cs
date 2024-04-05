namespace Blogs.Application.Contract.BlogApplication.Query;

public class BestBlogModel
{
    public int Id { get; set; }
    public string CategoryTitle { get; set; }
    public string CategorySlug { get; set; }
    public List<BestBlogForMagIndexQueryModel> Blogs { get; set; }
}