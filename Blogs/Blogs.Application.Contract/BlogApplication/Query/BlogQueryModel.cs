namespace Blogs.Application.Contract.BlogApplication.Query
{
    public class BlogQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int VisitCount { get; set; }
        public long UserId { get; set; }
        public string Writer { get; set; }
        public string UpdateDate { get; set; }
        public string CreationDate { get; set; }
        public bool Active { get; set; }
    }
}
