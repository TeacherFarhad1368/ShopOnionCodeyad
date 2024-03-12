using Blogs.Application.Contract.BlogCategoryApplication.Query;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Contract.BlogApplication.Command
{
    public interface IBlogApplication
    {
        OperationResult Create(CreateBlog command);
        OperationResult Edit(EditBlog command);
        bool ActivationChange(int id);
        bool VisitBlog(int id);
        EditBlog GetForEdit(int id);
    }
}
