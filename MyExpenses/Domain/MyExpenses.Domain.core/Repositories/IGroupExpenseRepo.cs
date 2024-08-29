using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.GroupExpense;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages GroupExpenses entities, extending the generic auditable repository interface.
    /// Provides methods for retrieving group expenses.
    /// </summary>
    public interface IGroupExpenseRepo : IAuditableRepo<GroupExpenses>
    {
        /// <summary>
        /// Retrieves the expenses associated with a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose expenses are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an IQueryable of ApiGroupExpense, representing the group's expenses.</returns>
        Task<IQueryable<ApiGroupExpense>> GetGroupExpenses(int groupId);

        /// <summary>
        /// Retrieves a specific group expense by its ID.
        /// </summary>
        /// <param name="groupExpenseId">The ID of the group expense to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an IQueryable of ApiGroupExpense, representing the specific group expense.</returns>
        Task<IQueryable<ApiGroupExpense>> GetGroupExpense(int groupExpenseId);
    }
}
