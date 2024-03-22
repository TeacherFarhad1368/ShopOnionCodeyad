using Shared.Application;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Domain.SiteServiceAgg;

namespace Site.Query.Services;

internal class SiteServiceQuery : ISiteServiceQuery
{
	private readonly ISiteServiceRepository _siteServiceRepository;

	public SiteServiceQuery(ISiteServiceRepository siteServiceRepository)
	{
		_siteServiceRepository = siteServiceRepository;
	}

	public List<SiteServiceAdminQueryModel> GetAllForAdmin() =>
		_siteServiceRepository.GetAllQuery()
		.Select(s => new SiteServiceAdminQueryModel
		(s.Id,s.Title,s.ImageName,s.ImageAlt,s.CreateDate.ToPersainDate(),s.Active)).ToList();
}
