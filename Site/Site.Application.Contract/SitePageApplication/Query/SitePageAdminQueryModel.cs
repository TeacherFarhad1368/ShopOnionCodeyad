namespace Site.Application.Contract.SitePageApplication.Query
{
	public class SitePageAdminQueryModel
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool Active { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
	}
}
