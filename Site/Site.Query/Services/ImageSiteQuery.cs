using Shared.Application;
using Site.Application.Contract.ImageSiteApplication.Query;
using Site.Domain.SiteImageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Query.Services
{
	internal class ImageSiteQuery : IImageSiteQuery
	{
		private readonly IImageSiteRepository _imageSiteRepository;

		public ImageSiteQuery(IImageSiteRepository imageSiteRepository)
		{
			_imageSiteRepository = imageSiteRepository;
		}

		public ImageAdminPaging GetAllForAdmin(int pageId, int take, string filter)
		{
			IQueryable<SiteImage> result;
			if (!string.IsNullOrEmpty(filter))
				result = _imageSiteRepository.GetAllByQuery(c => c.Title.Contains(filter));
			else
				result = _imageSiteRepository.GetAllQuery();

			ImageAdminPaging model = new();
			model.GetData(result, pageId, take, 5);
			model.Filter = filter;
			model.Images = new();
			if(result.Count() > 0)
				model.Images =  result.Skip(model.Skip).Take(model.Take).Select(s => new ImageSiteAdminQueryModel
				{
					CreateDate = s.CreateDate.ToPersainDate(),
					Id = s.Id,
					ImageName = FileDirectories.ImageDirectory100 +  s.ImageName,
					Title = s.Title
				}).ToList();

			return model;
		}
	}
}
