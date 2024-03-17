using Shared.Application;
using Site.Application.Contract.SitePageApplication.Query;
using Site.Domain.SitePageAgg;

namespace Site.Query.Services
{
	internal class SitePageQuery : ISitePageQuery
	{
		private readonly ISitePageRepository _sitePageRepository;

		public SitePageQuery(ISitePageRepository sitePageRepository)
		{
			_sitePageRepository = sitePageRepository;
		}

		public List<SitePageAdminQueryModel> GetAllForAdmin() =>
			_sitePageRepository.GetAllQuery().Select(p => new SitePageAdminQueryModel
			{
				Active = p.Active,
				CreateDate = p.CreateDate.ToPersainDate(),
				Id = p.Id,
				Slug = p.Slug,
				Title = p.Title,
				UpdateDate = p.UpdateDate.ToPersainDate()
			}).ToList();
	}
}
