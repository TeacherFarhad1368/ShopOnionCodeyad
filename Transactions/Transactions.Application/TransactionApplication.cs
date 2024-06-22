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
            Transaction transaction = new(command.UserId, command.Price, command.Portal);
            if (await _transactionRepository.CreateAsync(transaction))
                return new(true);
            return new OperationResult(false, ValidationMessages.SystemErrorMessage, nameof(command.Price));
        }

        public Task<bool> PaymentAsync(TransactionStatus status, int id, string refId)
        {
            throw new NotImplementedException();
        }
    }
}
