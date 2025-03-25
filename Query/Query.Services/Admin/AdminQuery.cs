using Blogs.Infrastructure;
using Comments.Infrastructure;
using PostModule.Infrastracture.EF;
using Query.Contract.Admin;
using Shop.Infrastructure;
using Transactions.Infrastructure;
using Users.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Query.Services.Admin;
class AdminQuery : IAdminQuery
{
    private readonly BlogContext _blogContext;  
    private readonly ShopContext _shopContext;
    private readonly CommentContext _commentContext;
    private readonly UserContext _userContext;
    private readonly Post_Context _postContext;
    private readonly TransactionContext _transactionContext;

    public AdminQuery(BlogContext blogContext, ShopContext shopContext,
        CommentContext commentContext, UserContext userContext, Post_Context postContext, TransactionContext transactionContext)
    {
        _blogContext = blogContext;
        _shopContext = shopContext;
        _commentContext = commentContext;
        _userContext = userContext;
        _postContext = postContext;
        _transactionContext = transactionContext;
    }

    public AdminDataQueryModel GetAdminData()
    {
        int blogCount = _blogContext.Blogs.Count();
        int blogCountComment = _commentContext.Comments.Where(c=>c.CommentFor == Shared.Domain.Enum.CommentFor.مقاله).Count();
        int blogCountVisit = _blogContext.Blogs.Sum(b => b.VisitCount);
        int orderCount = _shopContext.Orders.Where(c=>c.OrderStatus == Shared.Domain.Enum.OrderStatus.پرداخت_شده || c.OrderStatus == Shared.Domain.Enum.OrderStatus.ارسال_شده).Count();
        int orderItemCount = _shopContext.OrderItems.Include(i=>i.OrderSeller).Where(i=>i.OrderSeller.Status == Shared.Domain.Enum.OrderSellerStatus.پرداخت_شده || 
        i.OrderSeller.Status == Shared.Domain.Enum.OrderSellerStatus.در_حال_آماده_سازی || i.OrderSeller.Status == Shared.Domain.Enum.OrderSellerStatus.ارسال_شده).Count();
        int orderPostCount = _postContext.PostOrders.Where(p => p.Status == Shared.Domain.Enum.PostOrderStatus.پرداخت_شده).Count();
        int orderSellerCount = _shopContext.OrderSellers.Where(o => o.Status == Shared.Domain.Enum.OrderSellerStatus.پرداخت_شده 
        || o.Status == Shared.Domain.Enum.OrderSellerStatus.در_حال_آماده_سازی || o.Status == Shared.Domain.Enum.OrderSellerStatus.ارسال_شده).Count();
        int productCount = _shopContext.Products.Count();
        int productCountComment = _commentContext.Comments.Where(c => c.CommentFor == Shared.Domain.Enum.CommentFor.محصول).Count();
        int productCountSell = _shopContext.ProductSells.Count();
        int productCountVisit = _shopContext.ProductVisits.Sum(p => p.Count);
        int transactionCount = _transactionContext.Transactions.Where(t => t.Status == Shared.Domain.Enum.TransactionStatus.موفق).Count();
        int transactionSum = _transactionContext.Transactions.Where(t => t.Status == Shared.Domain.Enum.TransactionStatus.موفق).Sum(t => t.Price);
        int userCount = _userContext.Users.Count();
        int userCountDay = _userContext.Users.Where(u => u.CreateDate >= DateTime.Now.AddDays(-1)).Count();
        int userCountMounth = _userContext.Users.Where(u => u.CreateDate >= DateTime.Now.AddMonths(-1)).Count();
        int userCountWeek = _userContext.Users.Where(u => u.CreateDate >= DateTime.Now.AddDays(-7)).Count();
        AdminDataQueryModel model = new()
        {
            BlogCount = blogCount,
            BlogCountComment = blogCountComment,
            BlogCountVisit = blogCountVisit,
            OrderCount = orderCount,
            OrderItemCount = orderItemCount, 
            OrderPostCount = orderPostCount, 
            OrderSellerCount = orderSellerCount,
            ProductCount = productCount,
            ProductCountComment = productCountComment,
            ProductCountSell = productCountSell,
            ProductCountVisit = productCountVisit,
            TransactionCount = transactionCount,
            TransactionSum = transactionSum,
            UserCount = userCount,
            UserCountDay = userCountDay,   
            UserCountMounth = userCountMounth,
            UserCountWeek = userCountWeek
        };
        return model;   
    }
}
