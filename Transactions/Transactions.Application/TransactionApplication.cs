using Shared.Application;
using Shared.Domain.Enum;
using Transactions.Application.Contract;
using Transactions.Domain;

namespace Transactions.Application
{
    internal class TransactionApplication : ITransactionApplication
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionApplication(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<OperationResult> CreateAsync(CreateTransaction command)
        {
            if (command.Price < 1000)
                return new OperationResult(false, ValidationMessages.PaymentPriceError, nameof(command.Price));
            Transaction transaction = new(command.UserId, command.Price, command.Portal,command.TransactionFor,command.OwnerId);
            if (await _transactionRepository.CreateAsync(transaction))
                return new(true);
            return new OperationResult(false, ValidationMessages.SystemErrorMessage, nameof(command.Price));
        }

        public async Task<TransactionQueryModel> GetForCheckPaymentAsync(long id)
        {
            var tranaction = await _transactionRepository.GetByIdAsync(id);
            return new TransactionQueryModel(tranaction.Id,tranaction.UserId,tranaction.Price,tranaction.RefId,
                tranaction.Portal,tranaction.Status,tranaction.TransactionFor,tranaction.OwnerId);
        }

        public async Task<bool> PaymentAsync(TransactionStatus status, long id, string refId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            transaction.Payment(status, refId);
            return await _transactionRepository.SaveAsync();
        }
    }
}
