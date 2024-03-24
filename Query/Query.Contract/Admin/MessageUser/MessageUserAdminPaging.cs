using Shared;
using Shared.Domain.Enum;

namespace Query.Contract.Admin.MessageUser;

public class MessageUserAdminPaging : BasePaging
{
    public List<MessageUserAdminQueryModel> Messages { get; set; }
    public string Filter { get; set; }
    public MessageStatus Status { get; set; }
}
