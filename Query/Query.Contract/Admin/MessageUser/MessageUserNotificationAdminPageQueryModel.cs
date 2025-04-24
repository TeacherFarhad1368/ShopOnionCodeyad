namespace Query.Contract.Admin.MessageUser;

public class MessageUserNotificationAdminPageQueryModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserAvatar { get; set; }
    public string FullName { get; set; }
    public string Message { get; set; }
    public string CreationDate { get; set; }
}