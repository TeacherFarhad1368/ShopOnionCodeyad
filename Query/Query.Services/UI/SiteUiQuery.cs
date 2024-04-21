using Query.Contract.UI;
using Query.Contract.UI.Site;
using Seos.Domain;
using Shared.Domain.Enum;
using Site.Domain.SitePageAgg;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Services.UI;

internal class SiteUiQuery : ISiteUiQuery
{
    private readonly ISeoRepository _seoRepository;
    private readonly ISitePageRepository _sitePageRepository;
    private readonly ISiteSettingRepository _siteSettingRepository;

    public SiteUiQuery(ISeoRepository seoRepository, ISitePageRepository sitePageRepository,
        ISiteSettingRepository siteSettingRepository)
    {
        _seoRepository = seoRepository;
        _sitePageRepository = sitePageRepository;
        _siteSettingRepository = siteSettingRepository;
    }

    public AboutUsUiQueryModel GetAboutUsModelForUi()
    {
        var site = _siteSettingRepository.GetSingle();

        List<BreadCrumbQueryModel> breadcrums = new()
        {
             new BreadCrumbQueryModel(){Number = 1,Title = "صفحه اصلی",Url = "/"},
            new BreadCrumbQueryModel() {Number = 2,Title = "درباره ما",Url =""}
        };
        var seo = _seoRepository.GetSeoForUi(0, WhereSeo.About, "درباره ما");
        SeoUiQueryModel seoModel = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        return new AboutUsUiQueryModel(site.AboutTitle, site.AboutDescription, seoModel, breadcrums);
    }

    public ContactUsUiQueryModel GetContactUsModelForUi()
    {
        var site = _siteSettingRepository.GetSingle();

        List<BreadCrumbQueryModel> breadcrums = new()
        {
             new BreadCrumbQueryModel(){Number = 1,Title = "صفحه اصلی",Url = "/"},
            new BreadCrumbQueryModel() {Number = 2,Title = "تماس با ما",Url =""}
        };
        var seo = _seoRepository.GetSeoForUi(0, WhereSeo.Contact, "تماس با ما");
        SeoUiQueryModel seoModel = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        return new ContactUsUiQueryModel(site.ContactDescription, site.Phone1, site.Phone2, site.Email1, site.Email2, site.Address, seoModel, breadcrums);
    }

    public SitePageUiQueryModel GetSitePageQueryModel(string slug)
    {
        var site = _sitePageRepository.GetBySlug(slug);
        if (site == null) return null;
        var seo = _seoRepository.GetSeoForUi(site.Id, WhereSeo.Page, site.Title);
        SeoUiQueryModel seoModel  = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        List<BreadCrumbQueryModel> breadCrumbs = new()
        {
            new BreadCrumbQueryModel(){Number = 1,Title = "صفحه اصلی",Url = "/"},
            new BreadCrumbQueryModel() {Number = 2,Title = site.Title,Url =""}
        };

        return new SitePageUiQueryModel(site.Id, site.Title,site.Slug,site.Description, seoModel, breadCrumbs);  
    }
}
