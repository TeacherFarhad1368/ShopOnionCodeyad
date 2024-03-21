using Emails.Domain.SendEmailAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Infrastructure.EFConfigs
{
	internal class SendEmailConfig : IEntityTypeConfiguration<SendEmail>
	{
		public void Configure(EntityTypeBuilder<SendEmail> builder)
		{
			builder.ToTable("SendEmails");
			builder.HasKey(b => b.Id);
			builder.Property(b=>b.Title).IsRequired(true).HasMaxLength(255);
			builder.Property(b=>b.Text).IsRequired(true);
		}
	}
}
