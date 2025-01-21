using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing personal expenses.
    /// </summary>
    public interface ITransactionContract
    {
        Task<List<ApiTransaction>> GetTransactions(int accountId);
    }
}
