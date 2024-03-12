namespace Blogs.Application.Contract.BlogApplication.Command
{
    public class EditBlog : CreateBlog 
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
