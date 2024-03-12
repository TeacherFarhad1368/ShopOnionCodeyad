namespace Emails.Application.Contract.SensEmailApplication.Query
{
    public class SendEmailDetailQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
    }
}
