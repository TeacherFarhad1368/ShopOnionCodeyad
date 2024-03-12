namespace Blogs.Application.Contract.BlogApplication.Query
{
    public class AdminBlogsPageQueryModel
    {
        public int CategoryId { get; set; }
        public string PageTitle { get; set; }
        public List<BlogQueryModel> Blogs { get; set; }
    }
}
