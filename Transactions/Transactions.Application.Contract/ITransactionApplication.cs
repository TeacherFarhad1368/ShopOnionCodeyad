using Shared.Application;
using Shared.Domain.Enum;

namespace Transactions.Application.Contract
{
    public interface ITransactionApplication
    {
        Task<OperationResult> CreateAsync(CreateTransaction command);
        Task<bool> PaymentAsync(TransactionStatus status, int id,string refId);
    }
}
