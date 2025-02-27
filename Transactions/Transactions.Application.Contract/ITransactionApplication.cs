using Shared.Application;
using Shared.Domain.Enum;

namespace Transactions.Application.Contract
{
    public interface ITransactionApplication
    {
        Task<OperationResultWithKeylong> CreateAsync(CreateTransaction command);
        Task<bool> PaymentAsync(TransactionStatus status, long id,string refId);
        Task<bool> AddTransactionWalletId(long transactionId,int walletId);
        Task<TransactionQueryModel> GetForCheckPaymentAsync(string authority);
        Task<TransactionViewModel> GetTransactionForCheckPayment(long id);
    }
}
