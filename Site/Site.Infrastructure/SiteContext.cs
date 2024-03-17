using Microsoft.EntityFrameworkCore;
using Site.Domain.BanerAgg;
using Site.Domain.MenuAgg;
using Site.Domain.SiteImageAgg;
using Site.Domain.SitePageAgg;
using Site.Domain.SiteServiceAgg;
using Site.Domain.SiteSettingAgg;
using Site.Domain.SliderAgg;
using Site.Infrastructure.EFconfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options): base(options) 
        {
            
        }
        public DbSet<Baner> Baners { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<SiteService> SiteServices { get; set; }
        public DbSet<SitePage> SitePages { get; set; }
        public DbSet<SiteImage> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BanerConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
