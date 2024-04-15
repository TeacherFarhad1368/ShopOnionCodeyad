using Shared.Domain;
using Site.Application.Contract.SitePageApplication.Command;

namespace Site.Domain.SitePageAgg
{
	public interface ISitePageRepository : IRepository<int, SitePage>
	{
        SitePage GetBySlug(string slug);
        EditSitePage GetForEdit(int id);
	}
}
