namespace Users.Application.Contract.RoleApplication.Query
{
    public class RoleQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<UserRoleQueryModel> UserRoles { get; set; }
    }
}
