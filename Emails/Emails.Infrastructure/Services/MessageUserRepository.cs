using Emails.Domain.MessageUserAgg;
using Shared.Infrastructure;

namespace Emails.Infrastructure.Services;

internal class MessageUserRepository : Repository<int, MessageUser>, IMessageUserRepository
{
	private readonly EmailContext _context;

	public MessageUserRepository(EmailContext context) : base(context)
	{
		_context = context;
	}
}
