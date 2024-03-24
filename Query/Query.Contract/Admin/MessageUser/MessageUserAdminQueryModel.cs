using Shared.Domain.Enum;

namespace Query.Contract.Admin.MessageUser;

public class MessageUserAdminQueryModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UseName { get; set; }
	public MessageStatus Status { get; set; }
	public string FullName { get; set; }
	public string Subject { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public string CreationDate { get; set; }
}
