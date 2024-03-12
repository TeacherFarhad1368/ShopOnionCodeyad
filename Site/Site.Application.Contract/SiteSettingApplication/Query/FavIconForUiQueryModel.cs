namespace Site.Application.Contract.SiteSettingApplication.Query
{
    public class FavIconForUiQueryModel
    {
        public FavIconForUiQueryModel(string? favIcon)
        {
            FavIcon = favIcon;
        }

        public string? FavIcon { get; private set; }
    }
}
