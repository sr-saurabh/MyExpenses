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
    public interface IPersonalExpenseContract
    {
        /// <summary>
        /// Creates a new personal expense based on the provided details.
        /// </summary>
        /// <param name="expense">The details of the personal expense to create.</param>
        /// <returns>A <see cref="CreatePersonalExpense"/> object representing the created personal expense.</returns>
        Task<CreatePersonalExpense> CreatePersonalExpenses(CreatePersonalExpense expense);

        /// <summary>
        /// Retrieves a list of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose personal expenses are to be retrieved.</param>
        /// <returns>An <see cref="IEnumerable{ApiPersonalExpense}"/> representing the user's personal expenses.</returns>
        Task<IEnumerable<ApiPersonalExpense>> GetPersonalExpenses(int userId);

        /// <summary>
        /// Retrieves a list of personal expenses for a specific user with filtering options.
        /// </summary>
        /// <param name="userId">The ID of the user whose personal expenses are to be retrieved.</param>
        /// <param name="expenseFilter">The filter criteria to apply to the personal expenses.</param>
        /// <returns>An <see cref="ApiPersonalExpenseWithSummary"/> object representing the filtered personal expenses and summary.</returns>
        Task<ApiPersonalExpenseWithSummary> GetPersonalExpenses(int userId, PersonalExpenseFilter expenseFilter);

        /// <summary>
        /// Retrieves a specific personal expense by its ID.
        /// </summary>
        /// <param name="id">The ID of the personal expense to retrieve.</param>
        /// <returns>An <see cref="ApiPersonalExpense"/> object representing the personal expense.</returns>
        Task<ApiPersonalExpense> GetPersonalExpense(int id);

        /// <summary>
        /// Retrieves a summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the expense summary is to be retrieved.</param>
        /// <returns>A <see cref="PersonalExpenseSummary"/> object representing the summary of personal expenses.</returns>
        Task<PersonalExpenseSummary> GetPersonalExpenseSummary(int userId);

        /// <summary>
        /// Updates an existing personal expense with the provided details.
        /// </summary>
        /// <param name="expense">The updated personal expense details.</param>
        /// <returns><c>true</c> if the personal expense was successfully updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpdatePersonalExpense(UpdatePersonalExpense expense);

        /// <summary>
        /// Deletes a personal expense by its ID.
        /// </summary>
        /// <param name="id">The ID of the personal expense to delete.</param>
        /// <returns><c>true</c> if the personal expense was successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeletePersonalExpense(int id);
    }
}
