using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Blogs.Infrastructure.EFConfig;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Infrastructure
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogCategoryConfig());
            modelBuilder.ApplyConfiguration(new BlogConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}