using Blogs.Application.Contract.BlogApplication.Command;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.BlogAgg
{
    public interface IBlogRepository : IRepository<int, Blog>
    {
        Blog GetBySlug(string slug);
        EditBlog GetForEdit(int id);
    }
}
