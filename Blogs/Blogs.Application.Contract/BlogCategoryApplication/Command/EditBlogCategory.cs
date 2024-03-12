namespace Blogs.Application.Contract.BlogCategoryApplication.Command
{
    public class EditBlogCategory : CreateBlogCategory
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
