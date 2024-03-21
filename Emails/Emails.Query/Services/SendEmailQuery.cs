using Emails.Application.Contract.SensEmailApplication.Query;
using Emails.Domain.SendEmailAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Query.Services
{
	internal class SendEmailQuery : ISendEmailQuery
	{
		private readonly ISendEmailRepository _sendEmailRepository;
        public SendEmailQuery(ISendEmailRepository sendEmailRepository)
        {
            _sendEmailRepository = sendEmailRepository;
        }
        public List<SendEmailQueryModel> GetEmailSendsFoeAdmin()
		{
			return _sendEmailRepository.GetAllQuery()
				.Select(x => new SendEmailQueryModel()
				{
					CreationDate = x.CreateDate.ToPersainDate(),
					Id = x.Id,
					Title = x.Title
				}).ToList();
		}

		public SendEmailDetailQueryModel GetSendEmailDetailForAdmin(int id)
		{
			var email = _sendEmailRepository.GetById(id);
			return new()
			{
				CreationDate = email.CreateDate.ToPersainDate(),
				Id = email.Id,
				Text = email.Text,
				Title = email.Title
			};
		}
	}
}
