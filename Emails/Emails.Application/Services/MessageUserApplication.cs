using Emails.Application.Contract.MessageUserApplication.Command;
using Emails.Domain.MessageUserAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Services
{
    internal class MessageUserApplication : IMessageUserApplication
    {
        private readonly IMessageUserRepository _messageUserRepository;
        public MessageUserApplication(IMessageUserRepository messageUserRepository)
        {
            _messageUserRepository = messageUserRepository;
        }

        public bool AnswerByCall(int id)
        {
            var messageUser = _messageUserRepository.GetById(id);
            messageUser.AnswerByCall();
            return _messageUserRepository.Save();
        }

        public OperationResult AnsweredByEmail(int id, string mailMessage)
        {
            var messageUser = _messageUserRepository.GetById(id);
            //
            // send email
            //
            return new(false);
        }

        public OperationResult AnsweredBySMS(int id, string message)
        {
            var messageUser = _messageUserRepository.GetById(id);
            //
            // send sms
            //
            return new(false);
        }

        public OperationResult Create(CreateMessageUser command)
        {
            MessageUser messageUser = new(command.UserId, command.FullName, command.Subject, command.PhoneNumber, command.Email, command.Message);
            if (_messageUserRepository.Create(messageUser))
                return new(true);
            return new(false,ValidationMessages.SystemErrorMessage);  
        }
    }
}
