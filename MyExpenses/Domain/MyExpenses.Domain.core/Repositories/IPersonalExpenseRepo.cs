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
    public interface IPersonalExpenseRepo : IAuditableRepo<PersonalExpenses>
    {
        /// <summary>
        /// Retrieves a collection of personal expenses filtered by the specified criteria.
        /// </summary>
        /// <param name="id">The ID of the user or entity for which expenses are filtered.</param>
        /// <param name="expression">The filter criteria applied to the expenses.</param>
        /// <returns>An IQueryable collection of filtered PersonalExpenses.</returns>
        Task<IQueryable<PersonalExpenses>> GetFilteredExpenses(int id, PersonalExpenseFilter expression);

        /// <summary>
        /// Retrieves a summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the summary is retrieved.</param>
        /// <returns>A PersonalExpenseSummary object containing summarized expense data.</returns>
        Task<PersonalExpenseSummary> GetPersonalExpenseSummary(int userId);
    }
}
