namespace Query.Contract.UI.Site;

public class AboutUsUiQueryModel
{
    public AboutUsUiQueryModel(string? title, string? description, SeoUiQueryModel seo, List<BreadCrumbQueryModel> breadCrumbs)
    {
        Title = title;
        Description = description;
        Seo = seo;
        BreadCrumbs = breadCrumbs;
    }

    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public SeoUiQueryModel Seo { get; private set; }
    public List<BreadCrumbQueryModel> BreadCrumbs { get; private set; }
}