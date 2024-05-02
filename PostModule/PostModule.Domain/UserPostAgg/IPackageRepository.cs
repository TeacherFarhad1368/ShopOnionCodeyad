using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public interface IPackageRepository : IRepository<int, Package>
    {
        EditPackage GetForEdit(int id);
    }
}
