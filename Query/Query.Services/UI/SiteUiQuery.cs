using Query.Contract.UI;
using Query.Contract.UI.Site;
using Seos.Domain;
using Shared.Domain.Enum;
using Site.Domain.SitePageAgg;
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

    public SiteUiQuery(ISeoRepository seoRepository, ISitePageRepository sitePageRepository)
    {
        _seoRepository = seoRepository;
        _sitePageRepository = sitePageRepository;
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
