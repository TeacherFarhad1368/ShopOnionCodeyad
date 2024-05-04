using PostModule.Domain.SettingAgg;
using PostModule.Domain.UserPostAgg;
using Query.Contract.UI;
using Query.Contract.UI.PostPackage;
using Seos.Domain;
using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Services.UI
{
    internal class PackageUiQuery : IPackageUiQuery
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPostSettingRepository _postSettingRepository;
        private readonly ISeoRepository _seoRepository;

        public PackageUiQuery(IPackageRepository packageRepository, IPostSettingRepository postSettingRepository, ISeoRepository seoRepository)
        {
            _packageRepository = packageRepository;
            _postSettingRepository = postSettingRepository;
            _seoRepository = seoRepository;
        }

        public PackageUiPageQueryModel GetPackagesForUi()
        {
            var setting = _postSettingRepository.GetSingle();

            var seo = _seoRepository.GetSeoForUi(0, WhereSeo.PostPackage, "محاسبه هزینه مرسوله های پستی");

            SeoUiQueryModel seoModel = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords,
                seo.IndexPage, seo.Canonical, seo.Schema);

            List<BreadCrumbQueryModel> breadCrumbs = new()
            {
                new() {Number = 1,Title= "صفحه اصلی",Url = "/"},
                new() {Number = 2,Title = "محاسبه هزینه مرسوله های پستی" , Url = ""}
            };
            var packages = _packageRepository.GetAllByQuery(p => p.Active).OrderBy(p => p.Price)
                .Select(p => new PackageUiQueryModel(p.Id, p.Count, p.Price, p.Title, p.Description, 
                p.ImageAlt, $"{FileDirectories.PackageImageDirectory400}{p.ImageName}"))
                .ToList(); 

            return new(packages,breadCrumbs,setting.PackageTitle,
                setting.PackageDescription,seoModel);
        
    } 
    }
}
