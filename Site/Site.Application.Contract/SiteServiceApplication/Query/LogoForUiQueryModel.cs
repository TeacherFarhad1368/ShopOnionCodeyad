namespace Site.Application.Contract.SiteServiceApplication.Query
{
    public class LogoForUiQueryModel
    {
        public LogoForUiQueryModel(string? logoName, string? logoAlt)
        {
            LogoName = logoName;
            LogoAlt = logoAlt;
        }

        public string? LogoName { get; private set; }
        public string? LogoAlt { get; private set; }
    }
}
