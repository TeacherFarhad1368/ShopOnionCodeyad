using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Application.Contract.CommentApplication.Command
{
    public interface ICommentApplication
    {
        OperationResult Create(CreateComment command);
        OperationResult Reject(RejectComment command);
        bool AcceptedComment(long id);
    }
}
