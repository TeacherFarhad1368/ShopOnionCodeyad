using Shared.Domain.Enum;

namespace Query.Contract.Admin.Comment
{
	public class CommentAdminQueryModel
	{
		public long Id { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string CommentTitle { get; set; }
		public int OwnerId { get; set; }
		public CommentFor CommentFor { get; set; }
		public CommentStatus Status { get; set; }
		public string? WhyRejected { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string Text { get; set; }
		public long? ParentId { get; set; }
		public bool HaveChild { get; set; }
	}

}
