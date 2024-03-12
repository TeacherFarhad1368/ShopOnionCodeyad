using Emails.Application.Contract.SensEmailApplication.Command;
using Emails.Domain.SendEmailAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Services
{
    internal class SensEmailApplication : ISensEmailApplication
    {
        private readonly ISendEmailRepository _sendEmailRepository;
        public SensEmailApplication(ISendEmailRepository sendEmailRepository)
        {
            _sendEmailRepository = sendEmailRepository; 
        }
        public OperationResult Create(CreateSendEmail commmand)
        {
            // sendEmail and save
            throw new NotImplementedException();
        //}
    }
}
