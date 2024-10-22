using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contract.SellerPackegaApplication.Command
{
    public interface ISellerPackegaApplication
    {
        Task<OperationResult> CreateSellerPackageAsync(CreateSellerPackage command);
        Task<OperationResult> EditSellerPackageAsync(EditSellerPackage command);
        Task<EditSellerPackage> GetSellerPackageForEditAsync(int id);
        Task<OperationResult> CreateSellerPackageFeatureAsync(CreateSellerPackageFeature command);
        Task<bool> DeleteSellerPackageFeatureAsync(int id);
    }
}
