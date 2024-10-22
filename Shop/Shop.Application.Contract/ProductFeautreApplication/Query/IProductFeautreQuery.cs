using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductFeautreApplication.Query
{
    public interface IProductFeautreQuery
    {
        ProductFeaturePageAdminQueryModel GetProductFeaturesForAdmin(int productId);
    }
}
