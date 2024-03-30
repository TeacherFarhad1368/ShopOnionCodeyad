namespace Query.Contract.Admin.Email;

public class EnailUserAdminQueryModel
{
    public int Id { get; set; }
	public int UserId { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string CreationDate { get; set; }
	public bool Active { get; set; }
}