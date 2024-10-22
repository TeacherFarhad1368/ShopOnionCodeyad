using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductApplication.Command
{
    public interface IProductApplication
    {
        Task<OperationResult> CreateAsync(CreateProduct command);
        Task<OperationResult> EditAsync(EditProduct command);
        Task<EditProduct> GetForEditAsync(int id);
        Task<bool> ActivationChange(int id);
    }
}
