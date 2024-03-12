using Comments.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Infrastructure.Services
{
    internal class CommentRepository : Repository<long, Comment>, ICommentRepository
    {
        public CommentRepository(CommentContext context) : base(context)
        {
        }
    }
}
