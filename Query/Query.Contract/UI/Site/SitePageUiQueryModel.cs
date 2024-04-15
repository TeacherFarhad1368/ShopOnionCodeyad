namespace Query.Contract.UI.Site;

public class SitePageUiQueryModel
{
    public SitePageUiQueryModel(int id ,string title, string slug, string description, 
        SeoUiQueryModel seo, List<BreadCrumbQueryModel> breadCrumbs)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Description = description;
        Seo = seo;
        BreadCrumbs = breadCrumbs;
    }
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public SeoUiQueryModel Seo { get; private set; }
    public List<BreadCrumbQueryModel> BreadCrumbs { get; private set; }
}