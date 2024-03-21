using Emails.Domain.MessageUserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emails.Infrastructure.EFConfigs
{
	internal class MessageUserConfig : IEntityTypeConfiguration<MessageUser>
	{
		public void Configure(EntityTypeBuilder<MessageUser> builder)
		{
			builder.ToTable("MessageUsers");
			builder.HasKey(b => b.Id);
			builder.Property(b => b.Email).IsRequired(false).HasMaxLength(255);
			builder.Property(b => b.PhoneNumber).IsRequired(false).HasMaxLength(11);
			builder.Property(b => b.AnswerEmail).IsRequired(false);
			builder.Property(b => b.AnswerSms).IsRequired(false).HasMaxLength(300);
			builder.Property(b => b.Status).IsRequired(true);
			builder.Property(b => b.FullName).IsRequired(true).HasMaxLength(255);
			builder.Property(b => b.Subject).IsRequired(true).HasMaxLength(255);
			builder.Property(b => b.Message).IsRequired(true);
		}
	}
}
