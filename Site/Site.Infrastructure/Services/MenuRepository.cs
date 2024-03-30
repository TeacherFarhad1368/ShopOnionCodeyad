using Shared.Infrastructure;
using Site.Application.Contract.MenuApplication.Command;
using Site.Domain.MenuAgg;

namespace Site.Infrastructure.Services;

internal class MenuRepository : Repository<int, Menu>, IMenuRepository
{
    private readonly SiteContext _context;

    public MenuRepository(SiteContext context) : base(context)
    {
        _context = context;
    }

    public EditMenu GetForEdit(int id) =>
        _context.Menus.Select(s => new EditMenu
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Number  =s.Number,
            Title = s.Title,
            ParentId = s.ParentId == null ? 0 : s.ParentId.Value, 
            Url = s.Url
        }).SingleOrDefault(s => s.Id == id);
}
