using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using PostModule.Application.Contract.UserPostApplication.Command;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PackageRepository : Repository<int, Package>, IPackageRepository
{
    private readonly Post_Context _context;
    public PackageRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public EditPackage GetForEdit(int id)
    {
        return _context.Packages.Select(p => new EditPackage
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Count = p.Count,
            Price = p.Price,
            ImageAlt = p.ImageAlt,
            ImageFile = null,
            ImageName = p.ImageName
        }).SingleOrDefault(p => p.Id == id);
    }
}
