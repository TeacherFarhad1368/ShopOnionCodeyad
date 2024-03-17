using Microsoft.EntityFrameworkCore;
using Seos.Domain;

namespace Seos.Infrastructure
{
    public class Seo_Context : DbContext
    {
        public Seo_Context(DbContextOptions<Seo_Context> options) : base(options)
        {
        }
        public DbSet<Seo> Seos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SeoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
