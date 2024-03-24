using Comments.Application.Contract.CommentApplication.Query;
using Comments.Domain.CommentAgg;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Query
{
	internal class CommentQuery : ICommentQuery
	{
		private readonly ICommentRepository _commentRepository;

		public CommentQuery(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

	
	}
}
