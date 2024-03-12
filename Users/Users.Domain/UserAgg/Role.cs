using Shared.Domain;

namespace Users.Domain.UserAgg
{
    public class Role : BaseEntityCreate<int>
    {
        public string Title { get; private set; }
        public List<Permission> Permissions { get; private set; }
        public List<UserRole> UserRoles { get; private set; }
        public Role(string title)
        {
            Title = title;
        }
        public void Edit(string title)
        {
            Title = title;
        }
    }
}
