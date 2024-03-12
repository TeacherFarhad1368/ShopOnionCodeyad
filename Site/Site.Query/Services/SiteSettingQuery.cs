using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Domain.MenuAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Query.Services;

internal class SiteSettingQuery : ISiteSettingQuery
{
    public FavIconForUiQueryModel GetFavIconForUi()
    {
        throw new NotImplementedException();
    }

    public FooterUiQueryModel GetFooter()
    {
        throw new NotImplementedException();
    }

    public LogoForUiQueryModel GetLogoForUi()
    {
        throw new NotImplementedException();
    }

    public SocialForUiQueryModel GetSocialForUi()
    {
        throw new NotImplementedException();
    }
}
