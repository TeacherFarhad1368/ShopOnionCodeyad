using Shared.Infrastructure;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Domain.SiteServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services;

internal class SiteServiceRepository : Repository<int,SiteService> , ISiteServiceRepository
{
    private readonly SiteContext _context;

    public SiteServiceRepository(SiteContext context) : base(context)
    {
        _context = context;
    }

    public EditSiteService GetForEdit(int id) =>
        _context.SiteServices.Select(s => new EditSiteService
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Title = s.Title
        }).SingleOrDefault(s => s.Id == id);
}
