namespace Emails.Application.Contract.MessageUserApplication.Command
{
    public class CreateMessageUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Message { get; set; }
    }
}
