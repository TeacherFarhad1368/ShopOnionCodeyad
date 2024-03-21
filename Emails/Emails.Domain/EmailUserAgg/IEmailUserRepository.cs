
using Shared.Domain;

namespace Emails.Domain.EmailUserAgg
{
    public interface IEmailUserRepository : IRepository<int,EmailUser>
    {
        bool CreateList(List<EmailUser> emailUsers);
        EmailUser GetByEmail(string email);
    }
}
