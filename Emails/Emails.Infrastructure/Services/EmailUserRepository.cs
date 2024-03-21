using Emails.Domain.EmailUserAgg;
using Shared.Infrastructure;

namespace Emails.Infrastructure.Services;

internal class EmailUserRepository : Repository<int, EmailUser>, IEmailUserRepository
{
	private readonly EmailContext _context;

	public EmailUserRepository(EmailContext context) : base(context)
	{
		_context = context;
	}

	public bool CreateList(List<EmailUser> emailUsers)
	{
		_context.EmailUsers.AddRange(emailUsers);
		return Save();
	}

	public EmailUser GetByEmail(string email)
	{
		return _context.EmailUsers.SingleOrDefault(e => e.Email.Trim().ToLower() == email.ToLower().Trim());
	}
}
