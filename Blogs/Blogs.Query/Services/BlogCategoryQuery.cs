using Blogs.Application.Contract.BlogCategoryApplication.Query;
using Blogs.Domain.BlogCategoryAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Query.Services
{
    internal class BlogCategoryQuery : IBlogCategoryQuery
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryQuery(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public bool CheckCategoryHaveParent(int id)
        {
            var category = _blogCategoryRepository.GetById(id);
            return category.Parent > 0;       
        }

        public List<BlogCategoryForAddBlogQueryModel> GetCategoriesForAddBlog(int id)
        {
            return _blogCategoryRepository.GetAllByQuery(b => b.Parent == id).
                 Select(b => new BlogCategoryForAddBlogQueryModel
                 {
                     Id=b.Id,
                     Title=b.Title, 
                 }).ToList();
        }

        public BlogCategoryAsminPageQueryModel GetCategoriesForAdmin(int id)
        {
            BlogCategoryAsminPageQueryModel model = new() {
                Id = id ,
                Categories = _blogCategoryRepository.GetAllByQuery(c => c.Parent == id)
                   .Select(c => new BlogCategoryAdminQueryModel
                   {
                       Active = c.Active,
                       CreationDate = c.CreateDate.ToPersainDate(),
                       Id = c.Id,
                       ImageName = $"{FileDirectories.BlogCategoryImageDirectory100}{c.ImageName}",
                       Title = c.Title,
                       UpdateDate = c.UpdateDate.ToPersainDate()
                   }).ToList()
            };
            if (id > 0)
            {
                var category = _blogCategoryRepository.GetById(id);
                model.PageTitle = $"لیست زیر دسته های {category.Title}";
            }
            else
            {
                model.PageTitle = "لیست سر دسته های مقاله ";
            }
            return model;
        }

    }
}
