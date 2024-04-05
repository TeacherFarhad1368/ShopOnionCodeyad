using Shared;

namespace Query.Contract.UI.Blog;

public class BlogUiPaging : BasePaging
{
    public List<BlogCardQueryModel> Blogs { get; set; }
    public List<BreadCrumbQueryModel> BreadCrumb { get; set; }
    public List<BlogCategorySearchQueryModel> Categories { get; set; }
    public SeoUiQueryModel Seo { get; set; }
    public string Filter { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
}
