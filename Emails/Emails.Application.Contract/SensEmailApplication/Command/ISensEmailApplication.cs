using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Contract.SensEmailApplication.Command
{
    public interface ISensEmailApplication
    {
        OperationResult Create(CreateSendEmail commmand);
    }
}
