namespace Site.Application.Contract.SiteSettingApplication.Query;

public class FooterUiQueryModel
{
    public FooterUiQueryModel(string? enamad, string? samanDehi, string? title, string? description)
    {
        Enamad = enamad;
        SamanDehi = samanDehi;
        Description = description;
        Title = title;
    }

    public string? Enamad { get; private set; }
    public string? SamanDehi { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
}