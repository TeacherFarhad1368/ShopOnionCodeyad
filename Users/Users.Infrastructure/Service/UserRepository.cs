using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Users.Application.Contract.UserApplication.Command;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Infrastructure.Service;

internal class UserRepository : Repository<int,User> , IUserRepository
{
    private readonly UserContext _context;
    public UserRepository(UserContext context) : base(context)
    {
        _context = context;
    }

    public User GetByMobile(string v) =>
        _context.Users.SingleOrDefault(u => u.Mobile == v.Trim());

    public EditUserByAdmin GetForEditByAdmin(int userId)
    {
        var u = _context.Users.Find(userId);
        return new EditUserByAdmin()
        {
            AvatarFile = null,
            AvatarName = u.Avatar,
            Email = u.Email,
            FullName = u.FullName,
            Mobile = u.Mobile,
            UserGender = u.UserGender,
            Id = u.Id,  
            Password = null
        };
    }

    public EditUserByUser GetForEditByUser(int userId)
    {
        var u = _context.Users.Find(userId);
        return new EditUserByUser()
        {
            AvatarFile = null,
            AvatarName = u.Avatar,
            Email = u.Email,
            FullName = u.FullName,
            Mobile = u.Mobile,
            UserGender = u.UserGender
        };
    }
}
