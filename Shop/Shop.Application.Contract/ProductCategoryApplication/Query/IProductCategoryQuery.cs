using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductCategoryApplication.Query
{
    public interface IProductCategoryQuery
    {
        ProductCategoryAdminPageQueryModel GetCategoriesForAdmin(int id);
    }
}
