using Shared.Domain.Enum;

namespace Users.Application.Contract.RoleApplication.Command
{
    public class EditRole : CreateRole
    {
        public int Id { get; set; }
    }
}
