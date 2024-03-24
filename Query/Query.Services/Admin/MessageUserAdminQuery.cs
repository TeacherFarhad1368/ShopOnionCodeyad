using Emails.Domain.MessageUserAgg;
using Query.Contract.Admin.MessageUser;
using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.Admin
{
	internal class MessageUserAdminQuery : IMessageUserAdminQuery
	{
		private readonly IMessageUserRepository _messageUserRepository;
		private readonly IUserRepository _userRepository;

		public MessageUserAdminQuery(IMessageUserRepository messageUserRepository, IUserRepository userRepository)
		{
			_messageUserRepository = messageUserRepository;
			_userRepository = userRepository;
		}

		public MessageUserDetailAdminQueryModel GetMessageDetailForAdmin(int id)
		{
			var m = _messageUserRepository.GetById(id);
			MessageUserDetailAdminQueryModel model = new()
			{
				AnswerEmail = m.AnswerEmail,
				AnswerSms = m.AnswerSms,
				CreationDate = m.CreateDate.ToPersainDate(),
				Email = m.Email,
				FullName = m.FullName,	
				Id = id,
				Message =m.Message,
				PhoneNumber = m.PhoneNumber,
				Status = m.Status,
				Subject = m.Subject,
				UseName = "",
				UserId = m.UserId
			};
			if(model.UserId > 0)
			{
				var user = _userRepository.GetById(id);
				model.UseName = string.IsNullOrEmpty(user.FullName)  ? user.Mobile : user.FullName;	
			}
			return model;
		}

		public MessageUserAdminPaging GetMessagesForAdmin(int pageId, int take, string filter, MessageStatus status)
		{
			var result = _messageUserRepository.GetAllQuery().OrderByDescending(m=>m.Id);
			if (status != MessageStatus.همه)
				result = result.Where(r => r.Status == status).OrderByDescending(m => m.Id);
			if (string.IsNullOrEmpty(filter))
				result = result.Where(r => r.Email.ToLower().Contains(filter.ToLower()) || r.Message.ToLower().Contains(filter.ToLower())
				 || r.PhoneNumber.Contains(filter) || r.FullName.ToLower().Contains(filter.ToLower())).OrderByDescending(r => r.Id);

			MessageUserAdminPaging model = new();
			model.GetData(result, pageId, take, 5);
			model.Status = status;
			model.Filter = filter;
			model.Messages = new();
			if (result.Count() > 0)
				model.Messages = result.Skip(model.Skip).Take(model.Take)
					.Select(m => new MessageUserAdminQueryModel
					{
						CreationDate = m.CreateDate.ToPersainDate(),
						Email = m.Email,
						FullName = m.FullName,
						Id = m.Id,
						PhoneNumber = m.PhoneNumber,
						Status = m.Status,
						Subject = m.Subject,
						UseName ="",
						UserId = m.UserId
					}).OrderByDescending(m => m.Id).ToList();

			model.Messages.ForEach(x =>
			{
				if(x.UserId > 0)
				{
					var user = _userRepository.GetById(x.UserId);
					x.FullName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
				}
			});
			return model;
		}
	}
}
