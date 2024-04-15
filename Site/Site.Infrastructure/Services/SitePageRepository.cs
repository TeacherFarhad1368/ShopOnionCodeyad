using Shared.Infrastructure;
using Site.Application.Contract.SitePageApplication.Command;
using Site.Domain.SitePageAgg;

namespace Site.Infrastructure.Services;

internal class SitePageRepository : Repository<int, SitePage>, ISitePageRepository
{
	private readonly SiteContext _context;

	public SitePageRepository(SiteContext context) : base(context)
	{
		_context = context;
	}

    public SitePage GetBySlug(string slug)
    {
		return _context.SitePages.SingleOrDefault(s => s.Slug.Trim().ToLower() == slug.Trim().ToLower());
    }

    public EditSitePage GetForEdit(int id) =>
		_context.SitePages.Select(s => new EditSitePage
		{
			Id = s.Id,
            Slug = s.Slug,
            Text = s.Description,
            Title = s.Title
		}).SingleOrDefault(s => s.Id == id);
}
