using Shared;

namespace Site.Application.Contract.ImageSiteApplication.Query
{
	public class ImageSiteAdminQueryModel
	{
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string CreateDate { get; set; }
    }
    public class ImageAdminPaging : BasePaging
    {
        public string Filter { get; set; }
        public List<ImageSiteAdminQueryModel> Images { get; set; }
	}
}
