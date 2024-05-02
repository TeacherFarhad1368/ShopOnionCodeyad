using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using Microsoft.EntityFrameworkCore;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostOrderRepository : Repository<int, PostOrder>, IPOstOrderRepository
{
    private readonly Post_Context _context;
    public PostOrderRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public async Task<PostOrder> GetPostOrderNotPaymentForUserAsync(int userId) =>
        await _context.PostOrders.SingleOrDefaultAsync(p => p.UserId == userId && p.Status == Shared.Domain.Enum.PostOrderStatus.پرداخت_نشده);
}
