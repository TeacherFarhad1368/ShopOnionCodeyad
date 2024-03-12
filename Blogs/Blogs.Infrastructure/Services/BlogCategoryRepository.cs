using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Blogs.Domain.BlogCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Infrastructure.Services
{
    internal class BlogCategoryRepository : Repository<int, BlogCategory>, IBlogCategoryRepository
    {
        private readonly BlogContext _context;
        public BlogCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditBlogCategory GetForEdit(int id)
        {
            return _context.BlogCategories.Select(c => new EditBlogCategory
            {
                ImageAlt=c.ImageAlt,
                Id=c.Id,
                ImageFile=null,
                ImageName=c.ImageName,
                Parent=c.Parent,
                Slug=c.Slug,
                Title=c.Title
            }).SingleOrDefault(c => c.Id == id);
        }
    }
}
