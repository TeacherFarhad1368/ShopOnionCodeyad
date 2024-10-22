using Shop.Application.Contract.ProductFeautreApplication.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductGalleryApplication.Query
{
    public interface IProductGalleryQuery
    {
        ProductGalleryAdminPageQueryModel GetProductGalleriesForAdmin(int productId);
    }
}
