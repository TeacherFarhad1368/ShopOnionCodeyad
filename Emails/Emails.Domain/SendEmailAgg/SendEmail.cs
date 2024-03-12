using Shared.Domain;

namespace Emails.Domain.SendEmailAgg
{
    public class SendEmail : BaseEntityCreate<int>
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public SendEmail(string title,string text)
        {
            Title = title;
            Text = text;
        }
    }
}
