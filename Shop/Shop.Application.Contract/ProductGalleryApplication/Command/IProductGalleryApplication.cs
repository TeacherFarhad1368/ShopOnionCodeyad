using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductGalleryApplication.Command
{
    public interface IProductGalleryApplication
    {
        Task<OperationResult> CreateAsync(CreateProductGallery command);
        Task<bool> DeleteAsync(int id);
    }
}
