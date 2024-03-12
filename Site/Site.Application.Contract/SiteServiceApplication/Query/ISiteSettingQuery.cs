
namespace Site.Application.Contract.SiteServiceApplication.Query;
public interface ISiteSettingQuery
{
    SocialForUiQueryModel GetSocialForUi();
    LogoForUiQueryModel GetLogoForUi();
    FavIconForUiQueryModel GetFavIconForUi();
    FooterUiQueryModel GetFooter();
}
