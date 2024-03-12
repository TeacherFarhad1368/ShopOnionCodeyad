using Shared.Domain;
using Shared.Domain.Enum;

namespace Emails.Domain.MessageUserAgg
{
    public class MessageUser : BaseEntity<int>
    {
        public int UserId { get;private set; }
        public MessageStatus Status { get; private set; }
        public string FullName { get; private set; }
        public string Subject { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string Message { get; private set; }
        public string? AnswerSms { get; private set; }
        public string? AnswerEmail { get; private set; }
        public MessageUser(int userId,string fullName, string subject, string? phoneNumber,
            string? email, string message)
        {
            UserId = userId;
            Status = MessageStatus.دیده_نشده;
            FullName = fullName;
            Subject = subject;
            PhoneNumber = phoneNumber;
            Email = email;
            Message = message;
            AnswerSms = "";
            AnswerEmail = "";
        }
        public void AnswerSmsSend(string answerSms)
        {

            Status = MessageStatus.پاسخ_داده_شد_sms;
            AnswerSms = answerSms;
        }
        public void AnswerEmailSend(string answerEmail)
        {

            Status = MessageStatus.پاسخ_داده_شد_email;
            AnswerEmail = answerEmail;
        }
        public void AnswerByCall()
        {
            Status = MessageStatus.پاسخ_داده_شد;
        }
    }
}
