using Blogs.Application.Contract.BlogApplication.Command;
using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Blogs.Domain.BlogAgg;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Infrastructure.Services
{
    internal class BlogRepository : Repository<int,Blog> ,IBlogRepository
    {
        private readonly BlogContext _context;
        public BlogRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public Blog GetBySlug(string slug)
        {
            return _context.Blogs.SingleOrDefault(b => b.Slug.Trim() == slug.Trim());
        }

        public EditBlog GetForEdit(int id)
        {
            return _context.Blogs.Select(c => new EditBlog
            {
                ImageAlt = c.ImageAlt,
                Id = c.Id,
                ImageFile = null,
                ImageName = c.ImageName,
                Slug = c.Slug,
                Title = c.Title,
                CategoryId=c.CategoryId,
                ShortDescription=c.ShortDescription,
                Text = c.Text,
                SubCategoryId=c.SubCategoryId,
                UserId=c.UserId,
                Writer=c.Writer
            }).SingleOrDefault(c => c.Id == id);
        }
    }
}
