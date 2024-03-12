using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Contract.BlogCategoryApplication.Query
{
    public interface IBlogCategoryQuery
    {
        BlogCategoryAsminPageQueryModel GetCategoriesForAdmin(int id);
        List<BlogCategoryForAddBlogQueryModel> GetCategoriesForAddBlog(int id);
        bool CheckCategoryHaveParent(int id);
    }
    public class BlogCategoryAdminQueryModel
    {
        public int Id { get; set; }
        public string Title { get;  set; }
        public string ImageName { get;  set; }
        public string CreationDate { get;  set; }
        public string UpdateDate { get;  set; }
        public bool Active { get;  set; }
    }
    public class BlogCategoryAsminPageQueryModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public List<BlogCategoryAdminQueryModel> Categories { get; set; }
    }
    public class BlogCategoryForAddBlogQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
