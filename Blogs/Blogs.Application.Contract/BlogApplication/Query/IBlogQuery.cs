using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Contract.BlogApplication.Query;

public interface IBlogQuery
{
    AdminBlogsPageQueryModel GetBlogsForAdmin(int id);
    List<LastBlogForMagQueryModel> GetLastBlogForMagUi();
    List<BestBlogQueryModel> GetBestBlogForUi();
    List<BestBlogModel> GetBestBlogsForMagIndex();
}
public class BestBlogModel
{
    public int Id { get; set; }
    public string CategoryTitle { get; set; }
    public string CategorySlug { get; set; }
    public List<BestBlogForMagIndexQueryModel> Blogs { get; set; }
}