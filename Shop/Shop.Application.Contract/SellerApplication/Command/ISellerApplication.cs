using Shared.Application;
using Shared.Domain.Enum;

namespace Shop.Application.Contract.SellerApplication.Command;
public interface ISellerApplication
{
	Task<OperationResult> RequestSellerAsync(int userId,RequestSeller command);
	Task<OperationResult> EditRequestSellerAsync(int userId, EditSellerRequest command);
	Task<EditSellerRequest> GetRequsetFoeEditAsync(int id,int userId);
	Task<bool> ChangeStatusAsync(int id, SellerStatus status);
}
