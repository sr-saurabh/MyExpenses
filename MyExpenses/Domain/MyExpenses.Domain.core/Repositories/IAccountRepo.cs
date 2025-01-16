using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Repositories
{
    public interface IAccountRepo: IAuditableRepo<Account>
    {
        Task<bool> UpdateBankDetailsAsync(Account account);
        Task<bool> UpdateBalanceAsync(int accountId, decimal balance);
    }
}
