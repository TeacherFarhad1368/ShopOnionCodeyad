using Blogs.Application.Contract.BlogApplication.Query;
using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Query.Services
{
    internal class BlogQuery : IBlogQuery
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogQuery(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            _blogRepository = blogRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public List<BestBlogQueryModel> GetBestBlogForUi()
        {
            IQueryable<Blog> blogs = _blogRepository.GetAllByQuery(b => b.Active).OrderByDescending(b => b.VisitCount);
            var model = blogs.Select(b => new BestBlogQueryModel
            {
                CategorySlug = "",
                Category = b.CategoryId,
                CategoryTitle = "",
                CreationDate = b.CreateDate.ToPersainDate(),
                Slug = b.Slug,
                SubCategory = b.SubCategoryId,
                Title =b.Title,
                Visit = b.VisitCount,
                Writer = b.Writer,
                ImageName = FileDirectories.BlogImageDirectory + b.ImageName,
                ImageName400 = FileDirectories.BlogImageDirectory400 + b.ImageName,
                ImageAlt = b.ImageAlt
            }).OrderByDescending(b => b.Visit).Take(4).ToList();
            model.ForEach(x =>
            {
                if(x.SubCategory > 0)
                {
                    var sub = _blogCategoryRepository.GetById(x.SubCategory);
                    x.CategorySlug = sub.Slug;
                    x.CategoryTitle = sub.Title;
                }
                else
                {
                    var parent = _blogCategoryRepository.GetById(x.Category);
                    x.CategorySlug = parent.Slug;
                    x.CategoryTitle = parent.Title;
                }
            });
            return model;
        }

        public AdminBlogsPageQueryModel GetBlogsForAdmin(int id)
        {
            AdminBlogsPageQueryModel model = new()
            {
                CategoryId = id
            };
            if(id > 0)
            {
                var category = _blogCategoryRepository.GetById(id);
                model.PageTitle = $"لیست مقالات دسته بندی  {category.Title}";
                model.Blogs = _blogRepository.GetAllByQuery(b => b.CategoryId == id || b.SubCategoryId == id)
                .Select(b => new BlogQueryModel
                {
                    Active = b.Active,
                    CategoryId = b.SubCategoryId > 0 ? b.SubCategoryId : b.CategoryId,
                    CategoryTitle = "",
                    CreationDate = b.CreateDate.ToPersainDate(),
                    Id = b.Id,
                    ImageName =$"{FileDirectories.BlogImageDirectory100}{b.ImageName}",
                    Title = b.Title,
                    UpdateDate = b.UpdateDate.ToPersainDate(),
                    UserId = b.UserId,
                    VisitCount = b.VisitCount,
                    Writer = b.Writer
                }).ToList();
            }
            else
            {
                model.PageTitle = "لیست تمام مقالات  ";
                model.Blogs = _blogRepository.GetAllQuery()
                .Select(b => new BlogQueryModel
                {
                    Active = b.Active,
                    CategoryId = b.SubCategoryId > 0 ?b.SubCategoryId : b.CategoryId,
                    CategoryTitle = "",
                    CreationDate = b.CreateDate.ToPersainDate(),
                    Id = b.Id,
                    ImageName = $"{FileDirectories.BlogImageDirectory100}{b.ImageName}",
                    Title = b.Title,
                    UpdateDate = b.UpdateDate.ToPersainDate(),
                    UserId = b.UserId,
                    VisitCount = b.VisitCount,
                    Writer = b.Writer
                }).ToList();
            }
            model.Blogs.ForEach(x =>
            {
                x.CategoryTitle = _blogCategoryRepository.GetById(x.CategoryId).Title;
            });
            return model;
        }

        public List<LastBlogForMagQueryModel> GetLastBlogForMagUi()
        {
            IQueryable<Blog> blogs = _blogRepository.GetAllByQuery(b=>b.Active).OrderByDescending(b => b.CreateDate);
            return blogs.Select(b => new LastBlogForMagQueryModel(b.Slug, b.Title))
                .Take(5).ToList();
        }
    }
}
