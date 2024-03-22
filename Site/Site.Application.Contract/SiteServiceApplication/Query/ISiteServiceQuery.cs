namespace Site.Application.Contract.SiteServiceApplication.Query;
public interface ISiteServiceQuery
{
	List<SiteServiceAdminQueryModel> GetAllForAdmin();
}
public class SiteServiceAdminQueryModel
{
	public SiteServiceAdminQueryModel(int id, string title, string imageName, string imageAlt, string creationDate, bool active)
	{
		Id = id;
		Title = title;
		ImageName = imageName;
		ImageAlt = imageAlt;
		CreationDate = creationDate;
		Active = active;
	}

	public int Id { get; private set; }
    public string Title { get; private set; }
	public string ImageName { get; private set; }
	public string ImageAlt { get; private set; }
	public string CreationDate { get; private set; }
	public bool Active { get; private set; }
}