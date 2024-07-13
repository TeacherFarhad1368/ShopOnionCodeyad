using Shared;

namespace Query.Contract.Admin.User;

public class UsersForAdminPaging : BasePaging
{
    public string Filter { get; set; }
    public UserStatusSearch Status { get; set; }
    public UserOrderBySearch OrderBy { get; set; }
    public List<UserForAdminQueryModel> Users { get; set; }
}
public enum UserStatusSearch
{
    همه,
    حذف_شده_ها,
    کاربران_فعال,
    کاربران_غیر_فعال,
}
public enum UserOrderBySearch
{
    تاریخ_ثبت_از_آخر_به_اول,
    تاریخ_ثبت_از_اول_به_آخر
}
