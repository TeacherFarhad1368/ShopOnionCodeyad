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
    List<BestBlogSliderQueryModel> GetBestBlogForSliderUi();
    List<BestBlogModel> GetBestBlogsForMagIndex();
}
