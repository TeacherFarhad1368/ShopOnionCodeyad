using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Domain.EmailUserAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Services
{
    internal class EmailUserApplication : IEmailUserApplication
    {
        private readonly IEmailUserRepository _emailUserRepository;

        public EmailUserApplication(IEmailUserRepository emailUserRepository)
        {
            _emailUserRepository = emailUserRepository;
        }

        public bool ActivationChange(int id)
        {
            var email = _emailUserRepository.GetById(id);
            email.ActivationChange();
            return _emailUserRepository.Save();
        }

        public OperationResult Create(CreateEmailUser command)
        {
            if (_emailUserRepository.ExistBy(e => e.Email.Trim().ToLower() == command.Email.Trim().ToLower()))
                return new(false, ValidationMessages.DuplicatedMessage);
            EmailUser emailUser = new(command.Email.Trim().ToLower(), command.UserId);
            if (_emailUserRepository.Create(emailUser))
                return new(true);
            return new(false, ValidationMessages.SystemErrorMessage);
        }
    }
}
