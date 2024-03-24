using Emails.Domain.EmailUserAgg;
using Query.Contract.Admin.Email;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.Admin
{
	internal class EmailAdminQuery : IEmailAdminQuery
	{
		private readonly IEmailUserRepository _emailUserRepository;
		private readonly IUserRepository _userRepository;
		public EmailAdminQuery(IEmailUserRepository emailUserRepository)
		{
			_emailUserRepository = emailUserRepository;
		}

		public EmailUserAdminPaging GetEmailUsersForAdmin(int pageId, int take, string filter)
		{
			var result = _emailUserRepository.GetAllQuery().OrderByDescending(e=>e.Id);
			if (!string.IsNullOrEmpty(filter))
				result = result.Where(r => r.Email.ToLower().Contains(filter.ToLower())).OrderByDescending(r => r.Id);

			EmailUserAdminPaging model = new();
			model.GetData(result, pageId, take, 5);
			model.Filter = filter;
			model.Emails = new();
			if (result.Count() > 0)
				model.Emails = result.Skip(model.Skip).Take(model.Take).Select(e => new EnailUserAdminQueryModel
				{
					CreationDate = e.CreateDate.ToPersainDate(),
					Email = e.Email,
					Id = e.Id,
					UserId = e.UserId,
					UserName= ""
				}).OrderByDescending(e => e.Id).ToList();

			model.Emails.ForEach(x =>
			{
				if(x.UserId > 0)
				{
					var user = _userRepository.GetById(x.UserId);
					x.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
				}
			});
			return model;
		}
	}
}
