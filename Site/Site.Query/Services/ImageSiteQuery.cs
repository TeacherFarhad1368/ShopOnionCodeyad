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

		public List<ImageSiteAdminQueryModel> GetAllForAdmin() =>
			_imageSiteRepository.GetAllQuery().Select(s => new ImageSiteAdminQueryModel
			{
				CreateDate = s.CreateDate.ToPersainDate(),
				Id = s.Id,
				ImageName = s.ImageName,
				Title = s.Title
			}).ToList();
	}
}
