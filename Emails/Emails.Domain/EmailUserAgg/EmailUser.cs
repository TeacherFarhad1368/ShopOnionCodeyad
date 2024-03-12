using Shared.Domain;

namespace Emails.Domain.EmailUserAgg
{
    public class EmailUser : BaseEntity<int>
    {
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
        public EmailUser(string email, int userId = 0)
        {
            UserId = userId;
            Active = true;
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
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
    }
}
