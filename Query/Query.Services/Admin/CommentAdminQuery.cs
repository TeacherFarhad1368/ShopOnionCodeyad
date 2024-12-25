using Blogs.Domain.BlogAgg;
using Comments.Domain.CommentAgg;
using Query.Contract.Admin.Comment;
using Shared.Domain.Enum;
using Shop.Domain.ProductAgg;
using Site.Domain.SitePageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.Admin
{
	internal class CommentAdminQuery : ICommentAdminQuery
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IUserRepository _userRepository;
		private readonly IBlogRepository _blogRepository;
		private readonly ISitePageRepository _sitePageRepository ;
		private readonly IProductRepository _productRepository;
		public CommentAdminQuery(ICommentRepository commentRepository, IUserRepository userRepository,
			IBlogRepository blogRepository,ISitePageRepository sitePageRepository, IProductRepository productRepository)
		{
			_commentRepository = commentRepository;
			_userRepository = userRepository;
			_blogRepository = blogRepository;
			_sitePageRepository = sitePageRepository;
			_productRepository = productRepository;
		}

		public List<CommentAdminQueryModel> GetAllUnSeenCommentsForAdmin()
		{
			var comments = _commentRepository.GetAllByQuery(c => c.Status == CommentStatus.خوانده_نشده)
				.Select(c => new CommentAdminQueryModel
				{
					CommentFor = c.CommentFor,
					Email = c.Email,
					FullName = c.FullName,
					HaveChild = false,
					Id = c.Id,
					OwnerId = c.OwnerId,
					ParentId = c.ParentId,
					Status = c.Status,
					Text = c.Text,
					UserId = c.UserId,
					WhyRejected = c.WhyRejected,
					CommentTitle = "",
					UserName = ""
				}).ToList();
			comments.ForEach(x =>
			{
				x.HaveChild = _commentRepository.ExistBy(c => c.ParentId == x.Id);
				if (x.UserId > 0)
				{
					var user = _userRepository.GetById(x.UserId);
					x.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
				}
				switch (x.CommentFor)
				{
					case CommentFor.مقاله:
						var blog = _blogRepository.GetById(x.OwnerId);
						x.CommentTitle = $"نظر برای مقاله  {blog.Title}";
						break;
					case CommentFor.محصول:
						var product = _productRepository.GetById(x.OwnerId);
                        x.CommentTitle = $"نظر برای محصول  {product.Title}";
                        break;
                    case CommentFor.صفحه:
						var site = _sitePageRepository.GetById(x.OwnerId);
                        x.CommentTitle = $"نظر برای صفحه  {site.Title}";
                        break;
                    default:
						break;
				}

				// ببینیم نظر کدوم مقاله یا محصول است

			});
			return comments;
		}

		public CommentAdminPaging GetForAdmin(int pageId, int take, string filter, int ownerId,
		CommentFor commentFor, CommentStatus status, long? parentId)
		{
			var result = _commentRepository.GetAllByQuery(c => c.CommentFor == commentFor &&
			c.Status == status && c.OwnerId == ownerId && c.ParentId == parentId).OrderByDescending(c => c.Id);
			if (!string.IsNullOrEmpty(filter))
			{
				result = result.Where(c => c.FullName.Contains(filter) || c.Email.Contains(filter) || c.Text.Contains(filter)).OrderByDescending(c => c.Id);
			}
			CommentAdminPaging model = new();
			model.GetData(result, pageId, take, 5);
			model.Filter = filter;
			model.CommentFor = commentFor;
			model.CommentStatus = status;
			model.OwnerId = ownerId;
			model.ParentId = parentId;
			model.PageTitle = $"لیست نظرات - {status.ToString().Replace("_"," ")} - " +
				$"{commentFor.ToString().Replace("_"," ")}";
			model.Comments = new();
			if (result.Count() > 0)
				model.Comments = result.Skip(model.Skip).Take(model.Take).Select(c => new CommentAdminQueryModel
				{
					CommentFor = c.CommentFor,
					Email = c.Email,
					FullName = c.FullName,
					HaveChild = false,
					Id = c.Id,
					OwnerId = c.OwnerId,
					ParentId = c.ParentId,
					Status = c.Status,
					Text = c.Text,
					UserId = c.UserId,
					WhyRejected = c.WhyRejected,
					CommentTitle  ="",
					UserName = ""
				}).OrderByDescending(c => c.Id).ToList();

		  if(model.OwnerId > 0)
			{
                switch (model.CommentFor)
                {
                    case CommentFor.مقاله:
                        var blog = _blogRepository.GetById(model.OwnerId);
                        model.PageTitle = model.PageTitle +  $"  {blog.Title}";
                        break;
                    case CommentFor.محصول:
                        break;
                    case CommentFor.صفحه:
                        var site = _sitePageRepository.GetById( model.OwnerId);
                        model.PageTitle = model.PageTitle + $"  {site.Title}";
                        break;
                    default:
                        break;
                }
            }
		  if(model.ParentId != null &&  model.ParentId > 0)
			{
				var commment = _commentRepository.GetById(model.ParentId.Value);
				model.PageTitle = model.PageTitle + $"- پاسخ برای {commment.FullName}";

            }
				model.Comments.ForEach(x =>
			{
				x.HaveChild = _commentRepository.ExistBy(c => c.ParentId == x.Id);
				if(x.UserId > 0)
				{
					var user = _userRepository.GetById(x.UserId);
					x.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
				}

                switch (x.CommentFor)
                {
                    case CommentFor.مقاله:
                        var blog = _blogRepository.GetById(x.OwnerId);
                        x.CommentTitle = $"نظر برای مقاله  {blog.Title}";
                        break;
                    case CommentFor.محصول:
                        break;
                    case CommentFor.صفحه:
                        var site = _sitePageRepository.GetById(x.OwnerId);
                        x.CommentTitle = $"نظر برای صفحه  {site.Title}";
                        break;
                    default:
                        break;
                }

            });

			// تایین عنوان صفحه
			return model;

		}
	}
}
