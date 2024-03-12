namespace Site.Application.Contract.SiteSettingApplication.Query
{
    public class SocialForUiQueryModel
    {
        public SocialForUiQueryModel(string? instagram, string? whatsApp, string? telegram, string? youtube)
        {
            Instagram = instagram;
            WhatsApp = whatsApp;
            Telegram = telegram;
            Youtube = youtube;
        }

        public string? Instagram { get; private set; }
        public string? WhatsApp { get; private set; }
        public string? Telegram { get; private set; }
        public string? Youtube { get; private set; }
    }
}
