using Comments.Domain.CommentAgg;
using Query.Contract.UI.Comment;
using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.UI
{
    internal class CommentUiQuery : ICommentUiQuery
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentUiQuery(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public CommentUiPaging GetCommentsForUi(int ownerId, CommentFor commentFor, int pageId)
        {
            IQueryable<Comment> res = _commentRepository.GetAllByQuery(c => c.Status == CommentStatus.تایید_شده && c.OwnerId == ownerId && c.CommentFor == commentFor);
            CommentUiPaging model = new();
            model.GetData(res, pageId, 3, 2);
            model.OwnerId = ownerId;
            model.CommentFor = commentFor;
            model.Comments = new();
            if (res.Count() > 0)
                model.Comments = res.Where(r => r.ParentId == null).Skip(model.Skip).Take(model.Take)
                    .Select(c => new CommentUiQueryModel
                    {
                        Avatar = FileDirectories.UserImageDirectory100 + "default.png",
                        Childs = res.Where(r=>r.ParentId == c.Id).Select(r=> new CommentUiQueryModel
                        {
                            Avatar = FileDirectories.UserImageDirectory100 + "default.png",
                            Childs = new(),
                            CreationDate = r.CreateDate.ToPersainDate(),
                            FullName = r.FullName,
                            Id = r.Id,
                            Text = r.Text,
                            UserId = r.UserId
                        }).ToList(),
                        CreationDate = c.CreateDate.ToPersainDate(),
                        FullName = c.FullName,
                        Id = c.Id,
                        UserId = c.UserId,
                        Text = c.Text
                    }).ToList();

            if(model.Comments.Count() > 0)
            {
                foreach(var item in model.Comments)
                {
                    if(item.UserId > 0)
                    {
                        var userParent = _userRepository.GetById(item.UserId);
                        item.Avatar = FileDirectories.UserImageDirectory100 + userParent.Avatar;
                    }
                    if(item.Childs.Count() > 0)
                    {
                        foreach(var child in item.Childs)
                        {
                            if (child.UserId > 0)
                            {
                                var userChild = _userRepository.GetById(child.UserId);
                                item.Avatar = FileDirectories.UserImageDirectory100 + userChild.Avatar;
                            }
                        }
                    }
                }
            }

            return model;
        }
    }
}
