using Shared.Infrastructure;
using Site.Domain.SiteImageAgg;

namespace Site.Infrastructure.Services;

internal class ImageSiteRepository : Repository<int, SiteImage>, IImageSiteRepository
{
	private readonly SiteContext _context;

	public ImageSiteRepository(SiteContext context) : base(context)
	{
		_context = context;
	}
}