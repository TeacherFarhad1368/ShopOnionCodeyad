
using Seos.Application.Contract;
using Seos.Domain;
using Shared.Domain.Enum;

namespace Seos.Application.Services
{
    internal class SeoApplication : ISeoApplication
    {
        private readonly ISeoRepository _seoRepository;
        public SeoApplication(ISeoRepository seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public CreateSeo GetSeoForEdit(int ownerId, WhereSeo where)
        {
            return _seoRepository.GetSeoForUbsert(ownerId, where);
        }

        public bool UbsertSeo(CreateSeo command)
        {
            var seo = _seoRepository.GetSeo(command.OwnerId, command.Where);
            if (seo == null)
            {
                Domain.Seo seo1 = new(command.MetaTitle, command.MetaDescription, command.MetaKeyWords, command.IndexPage,
                    command.Canonical, command.Schema, command.Where, command.OwnerId);
                return _seoRepository.Create(seo1);
            }
            else
            {
                seo.Edit(command.MetaTitle, command.MetaDescription, command.MetaKeyWords, command.IndexPage,
                    command.Canonical, command.Schema);
                return _seoRepository.Save();
            }
        }


    }
}
