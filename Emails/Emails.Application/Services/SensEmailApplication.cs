using Emails.Application.Contract.SensEmailApplication.Command;
using Emails.Domain.SendEmailAgg;
using Microsoft.EntityFrameworkCore.Update.Internal;
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
            SendEmail email = new(commmand.Title, commmand.Text);
            if (_sendEmailRepository.Create(email)) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, nameof(commmand.Title));
        }
    }
}
