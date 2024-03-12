using Shared.Domain;
using Site.Application.Contract.SiteServiceApplication.Command;

namespace Site.Domain.SiteServiceAgg
{
    public interface ISiteServiceRepository : IRepository<int, SiteService>
    {
        EditSiteService GetForEdit(int id);
    }
}
