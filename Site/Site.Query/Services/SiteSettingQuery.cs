using Shared.Application;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Domain.MenuAgg;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Query.Services;

internal class SiteSettingQuery : ISiteSettingQuery
{
    private readonly ISiteSettingRepository _siteSettingRepository;

    public SiteSettingQuery(ISiteSettingRepository siteSettingRepository)
    {
        _siteSettingRepository = siteSettingRepository;
    }

    public FavIconForUiQueryModel GetFavIconForUi()
    {
        var site = _siteSettingRepository.GetSingle();
        return new FavIconForUiQueryModel(string.IsNullOrEmpty(site.FavIcon) ? "" : FileDirectories.SiteImageDirectory64 + site.FavIcon);
    }

    public FooterUiQueryModel GetFooter()
    {
        throw new NotImplementedException();
    }

    public LogoForUiQueryModel GetLogoForUi()
    {
        var site = _siteSettingRepository.GetSingle();
        return new LogoForUiQueryModel(string.IsNullOrEmpty(site.LogoName) ? "" : FileDirectories.SiteImageDirectory300 + site.LogoName, site.LogoAlt);
    }

    public SocialForUiQueryModel GetSocialForUi()
    {
        throw new NotImplementedException();
    }
}
