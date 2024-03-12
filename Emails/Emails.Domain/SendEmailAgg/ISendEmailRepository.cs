using Shared.Domain;

namespace Emails.Domain.SendEmailAgg
{
    public interface ISendEmailRepository : IRepository<int,SendEmail>
    {
    }
}
