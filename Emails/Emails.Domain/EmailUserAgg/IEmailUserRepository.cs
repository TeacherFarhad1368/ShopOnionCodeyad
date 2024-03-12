
using Shared.Domain;

namespace Emails.Domain.EmailUserAgg
{
    public interface IEmailUserRepository : IRepository<int,EmailUser>
    {
        void CreateList(List<EmailUser> emailUsers);
        List<EmailUserViewModel> GatAllForAdmin();
        EmailUser GetByEmail(string email);
    }
}
