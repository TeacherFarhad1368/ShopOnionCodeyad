using Shared.Domain;
using Site.Application.Contract.SitePageApplication.Command;

namespace Site.Domain.SitePageAgg
{
	public interface ISitePageRepository : IRepository<int, SitePage>
	{
		EditSitePage GetForEdit(int id);
	}
}
