namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public class EditSiteService : CreateSiteService
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
