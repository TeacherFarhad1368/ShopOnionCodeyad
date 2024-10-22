using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductCategoryApplication.Command
{
    public interface IProductCategoryApplication
    {
        Task<OperationResult> CreateAsync(CreateProductCategory command);
        Task<OperationResult> EditAsync(EditProductCategory command);
        Task<bool> ActivationChange(int id);
        Task<EditProductCategory> GetForEditAsync(int id);
    }
}
