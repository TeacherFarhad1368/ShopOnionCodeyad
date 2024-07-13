using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.WalletApplication.Command;

public interface IWalletApplication
{
    Task<OperationResult> DepositByUserAsync(CreateWalletWithWhy command);
    Task<OperationResult> DepositByAdminAsync(CreateWallet command);
    Task<OperationResult> WithdrawalAsync(CreateWalletWithWhy command);
    Task<bool> SuccessPaymentAsync(int id);
}
