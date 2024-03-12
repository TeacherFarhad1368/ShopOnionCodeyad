using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Contract.SensEmailApplication.Query
{
    public interface ISendEmailQuery
    {
        List<SendEmailQueryModel> GetEmailSendsFoeAdmin();
        SendEmailDetailQueryModel GetSendEmailDetailForAdmin(int id);
    }
}
