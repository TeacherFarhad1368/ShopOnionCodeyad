using Emails.Domain.SendEmailAgg;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Infrastructure.Services;

internal class SendEmailRepository : Repository<int,SendEmail> , ISendEmailRepository
{
	private readonly EmailContext _context;

	public SendEmailRepository(EmailContext context) : base(context)
	{
		_context = context;
	}
}
