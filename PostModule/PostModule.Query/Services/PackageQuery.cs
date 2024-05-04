using PostModule.Application.Contract.UserPostApplication.Query;
using PostModule.Domain.UserPostAgg;
using Shared.Application;

namespace PostModule.Query.Services
{
    public class PackageQuery : IPackageQuery
    {
        private readonly IPackageRepository _packageRepository;

        public PackageQuery(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public List<PackageAdminQueryModel> GetAll()
        {
            IQueryable<Package> model = _packageRepository.GetAllQuery();
            return model.Select(p => new PackageAdminQueryModel(p.Id, p.Title, p.Count, p.Price,
                p.CreateDate.ToPersainDate(), p.UpdateDate.ToPersainDate(), p.Active, FileDirectories.PackageImageDirectory100 + p.ImageName))
            .ToList();
        }
    }
}