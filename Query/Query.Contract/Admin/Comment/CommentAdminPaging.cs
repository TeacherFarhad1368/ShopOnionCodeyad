using Shared.Domain.Enum;
using Shared;

namespace Query.Contract.Admin.Comment
{
	public class CommentAdminPaging : BasePaging
	{
		public List<CommentAdminQueryModel> Comments { get; set; }
		public string Filter { get; set; }
		public CommentFor CommentFor { get; set; }
		public CommentStatus CommentStatus { get; set; }
		public int OwnerId { get; set; }
		public long? ParentId { get; set; }
		public string PageTitle { get; set; }
	}

}
