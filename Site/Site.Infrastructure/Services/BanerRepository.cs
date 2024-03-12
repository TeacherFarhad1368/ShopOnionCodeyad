using Shared.Infrastructure;
using Site.Application.Contract.BanerApplication.Command;
using Site.Domain.BanerAgg;

namespace Site.Infrastructure.Services;

internal class BanerRepository : Repository<int, Baner>, IBanerRepository
{
    private readonly SiteContext _context;

    public BanerRepository(SiteContext context) : base(context)
    {
        _context = context;
    }

    public EditBaner GetForEdit(int id) =>
        _context.Baners.Select(s => new EditBaner
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Url = s.Url
        }).SingleOrDefault(s => s.Id == id);
}
