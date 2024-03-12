using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Contract.MessageUserApplication.Command
{
    public interface IMessageUserApplication
    {
        OperationResult Create(CreateMessageUser command);
        OperationResult AnsweredBySMS(int id,string message);
        OperationResult AnsweredByEmail(int id,string mailMessage);
        bool AnswerByCall(int id);
    }
}
