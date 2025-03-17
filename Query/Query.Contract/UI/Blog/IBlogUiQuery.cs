using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UI.Blog;

public interface IBlogUiQuery
{
    BlogUiPaging GetBlogsForUi(string slug, int pageId, string filter);
    SingleBlogQueryModel GetSingleBlogForUi(string slug);
    List<BlogSearchAjaxModel> SearchAjax(string filter, int count);
}
public class BlogSearchAjaxModel
{
    public string ImageAddress { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int id { get; set; }
}
