using Seos.Application.Contract;
using Seos.Domain;
using Shared.Domain.Enum;

namespace Seos.Query;
class SeoQuery : ISeoQuery
{
    private readonly ISeoRepository _repository;

    public SeoQuery(ISeoRepository repository)
    {
        _repository = repository;
    }

    public SeoQueryModel GetSeo(int ownerId, WhereSeo where, string title)
    {
        var seo = _repository.GetSeoForUi(ownerId, where, title);
        return new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
    }
}
