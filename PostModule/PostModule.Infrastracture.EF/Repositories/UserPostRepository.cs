using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using Microsoft.EntityFrameworkCore;

namespace PostModule.Infrastracture.EF.Repositories;

internal class UserPostRepository : Repository<int, UserPost>, IUserPostRepository
{
    private readonly Post_Context _context;
    public UserPostRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public async Task<UserPost> GetByApiCode(string apiCode)
    {
        return await _context.UserPosts.SingleOrDefaultAsync(p => p.ApiCode == apiCode);
    }

    public async Task<UserPost> GetForUser(int userId)
    {
        UserPost userPost = await _context.UserPosts.SingleOrDefaultAsync(p => p.UserId == userId);
        if(userPost == null)
        {
            userPost = new UserPost(userId, 50, Guid.NewGuid().ToString());
            await _context.UserPosts.AddAsync(userPost);
            await _context.SaveChangesAsync();
        }
        return userPost;
    }
}