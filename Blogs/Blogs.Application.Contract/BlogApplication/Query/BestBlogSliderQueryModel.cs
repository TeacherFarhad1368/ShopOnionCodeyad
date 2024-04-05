namespace Blogs.Application.Contract.BlogApplication.Query;

public class BestBlogSliderQueryModel
{
    public int Id { get; set; }
    public int VisitCount { get; set; }
    public string title { get; set; }
    public string slug { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
}