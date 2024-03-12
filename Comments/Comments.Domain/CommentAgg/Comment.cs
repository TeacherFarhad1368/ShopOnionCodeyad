using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Domain.CommentAgg
{
    public class Comment : BaseEntityCreate<long>
    {
        public Comment(int userId,int ownerId, CommentFor commentFor, 
            string? fullName, string? email, string? subject, string text, long? parentId)
        {
            UserId = userId;
            OwnerId = ownerId;
            CommentFor = commentFor;
            FullName = fullName;
            Email = email;
            Subject = subject;
            Text = text;
            ParentId = parentId;
            Status = CommentStatus.خوانده_نشده;
            WhyRejected = null;
        }
        public Comment()
        {
            Childs = new();
        }
        public void RejectedComment(string why)
        {
            Status = CommentStatus.رد_شده;
            WhyRejected = why;
        }
        public void AcceptedComment()
        {
            Status = CommentStatus.تایید_شده; 
        }
        public int UserId { get; private set; }
        public int OwnerId { get; private set; }
        public CommentFor CommentFor { get; private set; }
        public CommentStatus Status { get; private set; }
        public string? WhyRejected { get; private set; }
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public string? Subject { get; private set; }
        public string Text { get; private set; }
        public long? ParentId { get; private set; }
        public Comment? Parent { get; private set; }
        public List<Comment> Childs { get; private set; }
    }
}
