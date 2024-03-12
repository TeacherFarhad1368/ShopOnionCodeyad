using Shared.Infrastructure;
using Site.Application.Contract.SliderApplication.Command;
using Site.Domain.SliderAgg;

namespace Site.Infrastructure.Services;

internal class SliderRepository : Repository<int, Slider>, ISliderRepository
{
    private readonly SiteContext _context;

    public SliderRepository(SiteContext context) : base(context)
    {
        _context = context;
    }

    public EditSlider GetForEdit(int id) =>
        _context.Sliders.Select(s => new EditSlider
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName
        }).SingleOrDefault(s => s.Id == id);
}
