using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages PersonalExpenses entities,
    /// extending the generic auditable repository interface.
    /// </summary>
    public interface ITransactionRepo : IAuditableRepo<Transaction>
    {

    }
}
