using Blogs.Application.Contract.BlogCategoryApplication.Command;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.BlogCategoryAgg
{
    public interface IBlogCategoryRepository : IRepository<int, BlogCategory>
    {
        EditBlogCategory GetForEdit(int id);
    }
}
