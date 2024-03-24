using Shared;

namespace Query.Contract.Admin.Email;

public class EmailUserAdminPaging : BasePaging
{
    public List<EnailUserAdminQueryModel> Emails { get; set; }
    public string Filter { get; set; }
}
