using Site.Application.Contract.SiteSettingApplication.Command;

namespace Site.Domain.SiteSettingAgg
{
    public interface ISiteSettingRepository
    {
        UbsertSiteSetting GetForUbsert();
        SiteSetting GetSingle();
        bool Save();
    }
}
