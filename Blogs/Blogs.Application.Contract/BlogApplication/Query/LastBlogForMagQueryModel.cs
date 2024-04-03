namespace Blogs.Application.Contract.BlogApplication.Query
{
    public class LastBlogForMagQueryModel
    {
        public LastBlogForMagQueryModel(string slug, string title)
        {
            Slug = slug;
            Title = title;
        }

        public string Slug { get; private set; }
        public string Title { get; private set; }
    }
}
