using Microsoft.EntityFrameworkCore;
using PostModule.Domain.StateEntity;
using PostModule.Domain.CityEntity;
using PostModule.Infrastracture.EF.Mapping;
using PostModule.Domain.PostEntity;
using PostModule.Domain.UserPostAgg;

namespace PostModule.Infrastracture.EF
{
    public class Post_Context : DbContext
    {
        public Post_Context(DbContextOptions<Post_Context> options) : base(options)
        {

        }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPrice> PostPrices { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PostOrder> PostOrders { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StateMapping).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
