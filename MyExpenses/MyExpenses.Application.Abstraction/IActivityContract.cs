using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    public interface IActivityContract
    {
        /// <summary>
        /// Defines the contract for managing personal expenses.
        /// </summary>
        public interface ITransactionContract
        {
            /// <summary>
            /// Creates a new personal expense based on the provided details.
            /// </summary>
            /// <param name="expense">The details of the personal expense to create.</param>
            /// <returns>A <see cref="CreateActivity"/> object representing the created personal expense.</returns>
            Task<CreateActivity> CreateActivityAsync(CreateActivity expense);

            /// <summary>
            /// Retrieves a list of personal expenses for a specific user.
            /// </summary>
            /// <param name="userId">The ID of the user whose personal expenses are to be retrieved.</param>
            /// <returns>An <see cref="IEnumerable{ApiPersonalExpense}"/> representing the user's personal expenses.</returns>
            Task<List<ApiActivity>> GetActivitiesAsync(int userId);

            /// <summary>
            /// Retrieves a list of personal expenses for a specific user with filtering options.
            /// </summary>
            /// <param name="userId">The ID of the user whose personal expenses are to be retrieved.</param>
            /// <param name="expenseFilter">The filter criteria to apply to the personal expenses.</param>
            /// <returns>An <see cref="ApiPersonalExpenseWithSummary"/> object representing the filtered personal expenses and summary.</returns>
            Task<ApiActivityWithSummary> GetPersonalExpenses(int userId, PersonalExpenseFilter expenseFilter);

            /// <summary>
            /// Retrieves a specific personal expense by its ID.
            /// </summary>
            /// <param name="id">The ID of the personal expense to retrieve.</param>
            /// <returns>An <see cref="ApiActivity"/> object representing the personal expense.</returns>
            Task<ApiActivity> GetPersonalExpense(int id);

            /// <summary>
            /// Retrieves a summary of personal expenses for a specific user.
            /// </summary>
            /// <param name="userId">The ID of the user for whom the expense summary is to be retrieved.</param>
            /// <returns>A <see cref="ActivitySummary"/> object representing the summary of personal expenses.</returns>
            Task<ActivitySummary> GetPersonalExpenseSummary(int userId);

            /// <summary>
            /// Updates an existing personal expense with the provided details.
            /// </summary>
            /// <param name="expense">The updated personal expense details.</param>
            /// <returns><c>true</c> if the personal expense was successfully updated; otherwise, <c>false</c>.</returns>
            Task<bool> UpdatePersonalExpense(UpdateActivity expense);

            /// <summary>
            /// Deletes a personal expense by its ID.
            /// </summary>
            /// <param name="id">The ID of the personal expense to delete.</param>
            /// <returns><c>true</c> if the personal expense was successfully deleted; otherwise, <c>false</c>.</returns>
            Task<bool> DeletePersonalExpense(int id);

            /// <summary>
            /// Get the list of categories
            /// </summary>
            /// <param name="appUserId"></param>
            /// <returns> Return the list of categories </returns>
            Task<List<string>> GetCategories(int appUserId);

            /// <summary>
            /// Get the list of categories
            /// </summary>
            /// <param name="appUserId"></param>
            /// <returns> Return the weekly expense</returns>
            Task<WeeklyExpense> GetWeeklyExpense(int appUserId, bool isCurrent);
        }
}
