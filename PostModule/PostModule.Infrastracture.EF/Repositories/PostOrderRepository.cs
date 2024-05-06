using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Application;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostOrderRepository : Repository<int, PostOrder>, IPOstOrderRepository
{
    private readonly Post_Context _context;
    public PostOrderRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public async Task<PostOrderUserPanelModel> GetPostOrderNotPaymentForUser(int userId)
    {
        var postOrder = await GetPostOrderNotPaymentForUserAsync(userId);
        if (postOrder == null) return null;
        var package = await _context.Packages.FindAsync(postOrder.PackageId);
        if(package.Price != postOrder.Price)
        {
            postOrder.Edit(package.Id, package.Price);
            await _context.SaveChangesAsync();
        }
        return new PostOrderUserPanelModel(postOrder.Id, postOrder.PackageId, package.Title, postOrder.Price,
            $"{FileDirectories.PackageImageDirectory400}{package.ImageName}", package.ImageAlt, package.Count,package.Description);
    }

    public async Task<PostOrder> GetPostOrderNotPaymentForUserAsync(int userId) =>
        await _context.PostOrders.SingleOrDefaultAsync(p => p.UserId == userId 
        && p.Status == Shared.Domain.Enum.PostOrderStatus.پرداخت_نشده);
}
