using Comments.Application.Contract.CommentApplication.Command;
using Comments.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Application.Services
{
    internal class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool AcceptedComment(long id)
        {
            var comment = _commentRepository.GetById(id);
            comment.AcceptedComment();
            return _commentRepository.Save();
        }

        public OperationResult Create(CreateComment command)
        {
            Comment comment = new(command.UserId, command.OwnerId, command.For,
                command.FullName, command.Email, command.Subject, command.Text, command.ParentId);
            if (_commentRepository.Create(comment)) return new(true);
            return new(false,ValidationMessages.SystemErrorMessage);
        }

        public OperationResult Reject(RejectComment command)
        {
            var comment = _commentRepository.GetById(command.Id);
            comment.RejectedComment(command.Why);
            if (_commentRepository.Save())
                return new(true);
            return new(false,ValidationMessages.SystemErrorMessage);
        }
    }
}
