namespace Query.Contract.UI.PostPackage;

public class PackageUiPageQueryModel
{
    public PackageUiPageQueryModel(List<PackageUiQueryModel> packages,
        List<BreadCrumbQueryModel> breadCrumbs, string? title, string? description, SeoUiQueryModel seo)
    {
        Packages = packages;
        BreadCrumbs = breadCrumbs;
        Title = title;
        Description = description;
        Seo = seo;
    }

    public List<PackageUiQueryModel> Packages { get; private set; }
    public List<BreadCrumbQueryModel> BreadCrumbs { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public SeoUiQueryModel Seo { get; private set; }
    
}
