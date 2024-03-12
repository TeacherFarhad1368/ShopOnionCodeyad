namespace Site.Application.Contract.SiteSettingApplication.Query;

public class FooterUiQueryModel
{
    public FooterUiQueryModel(string? enamad, string? samanDehi, string? seoBox, string? android)
    {
        Enamad = enamad;
        SamanDehi = samanDehi;
        SeoBox = seoBox;
        Android = android;
    }

    public string? Enamad { get; private set; }
    public string? SamanDehi { get; private set; }
    public string? SeoBox { get; private set; }
    public string? Android { get; private set; }
}