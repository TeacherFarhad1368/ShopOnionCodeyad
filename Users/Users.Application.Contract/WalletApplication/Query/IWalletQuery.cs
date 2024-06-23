using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Contract.WalletApplication.Query;

public interface IWalletQuery
{
    int GetWalletSum(int userId);

}