namespace Emails.Application.Contract.EmailUserApplication.Command
{
    public class CreateEmailUser
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
