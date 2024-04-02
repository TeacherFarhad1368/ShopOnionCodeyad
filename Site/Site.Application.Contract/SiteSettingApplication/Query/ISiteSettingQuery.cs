using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SiteSettingApplication.Query;

public interface ISiteSettingQuery
{
    SocialForUiQueryModel GetSocialForUi();
    LogoForUiQueryModel GetLogoForUi();
    FavIconForUiQueryModel GetFavIconForUi();
    FooterUiQueryModel GetFooter();
    ContactFooterUiQueryModel GetContactDataForFooter();
}
