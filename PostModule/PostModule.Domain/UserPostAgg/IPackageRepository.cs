using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public interface IPackageRepository : IRepository<int, Package>
    {
        Task<CreatePostOrder> GetCreatePostModelAsync(int userId, int packageId);
        EditPackage GetForEdit(int id);
    }
}
