using Shared.Domain;

namespace Emails.Domain.EmailUserAgg
{
    public class EmailUser : BaseEntityCreateActive<int>
    {
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public EmailUser(string email, int userId = 0)
        {
            UserId = userId;
            Email = email;
        }
        public void AddUserId(int userId)
        {
            UserId = userId;
        }
        public void EditEmail(string email)
        {
            Email = email;
        }
    }
}
