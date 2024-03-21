using Emails.Domain.EmailUserAgg;
using Emails.Domain.MessageUserAgg;
using Emails.Domain.SendEmailAgg;
using Emails.Infrastructure.EFConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Infrastructure
{
	public class EmailContext : DbContext
	{
        public EmailContext(DbContextOptions<EmailContext> options): base(options)
        {
            
        }
        public DbSet<EmailUser> EmailUsers { get; set; }
        public DbSet<SendEmail> SendEmails { get; set; }
        public DbSet<MessageUser> MessageUsers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SendEmailConfig).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
