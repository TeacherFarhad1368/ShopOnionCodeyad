using Shared.Domain;
using Site.Application.Contract.BanerApplication.Command;

namespace Site.Domain.BanerAgg
{
    public interface IBanerRepository : IRepository<int, Baner>
    {
        EditBaner GetForEdit(int id);
    }
}
