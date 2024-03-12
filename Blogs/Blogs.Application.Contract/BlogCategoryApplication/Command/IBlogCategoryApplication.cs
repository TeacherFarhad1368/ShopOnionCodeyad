using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blogs.Application.Contract.BlogCategoryApplication.Command
{
    public interface IBlogCategoryApplication
    {
        OperationResult Create(CreateBlogCategory command);
        OperationResult Edit(EditBlogCategory command);
        bool ActivationChange(int id);
        EditBlogCategory GetForEdit(int id);
    }
}
