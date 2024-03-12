using Microsoft.EntityFrameworkCore;
using Users.Domain.UserAgg;
using Users.Infrastructure.EFConfigs;

namespace Users.Infrastructure
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) :base(options)
        {

        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(UserConfig).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}