using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.ProductFeautreApplication.Command
{
    public interface IProductFeautreApplication
    {
        Task<OperationResult> CreateAsync(CreateProductFeautre command);
        Task<bool> DeleteAsync(int id);
    }
}
