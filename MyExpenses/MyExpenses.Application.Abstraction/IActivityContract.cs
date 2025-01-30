using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing personal expenses.
    /// </summary>
    public interface IActivityContract
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
        Task<ApiActivityWithSummary> GetActivityWithSummary(int userId, ActivityFilter activityFilter);

        /// <summary>
        /// Retrieves a specific personal expense by its ID.
        /// </summary>
        /// <param name="id">The ID of the personal expense to retrieve.</param>
        /// <returns>An <see cref="ApiActivity"/> object representing the personal expense.</returns>
        Task<ApiActivity> GetActivity(int id);

        /// <summary>
        /// Retrieves a summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the expense summary is to be retrieved.</param>
        /// <returns>A <see cref="ActivitySummary"/> object representing the summary of personal expenses.</returns>
        Task<ActivitySummary> GetActivitySummary(int userId);

        /// <summary>
        /// Updates an existing personal expense with the provided details.
        /// </summary>
        /// <param name="expense">The updated personal expense details.</param>
        /// <returns><c>true</c> if the personal expense was successfully updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateActivity(UpdateActivity expense);

        /// <summary>
        /// Deletes a personal expense by its ID.
        /// </summary>
        /// <param name="id">The ID of the personal expense to delete.</param>
        /// <returns><c>true</c> if the personal expense was successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteActivity(int id);

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
        Task<WeeklyExpense> GetWeeklyActivity(int appUserId, bool isCurrent);

        /// <summary>
        /// Get the list of Expenses by category name
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<List<ApiActivity>> GetExpenseByCategory(int appUserId, string category);


        /// <summary>
        /// Get the list of Expenses by category name
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<DailyActivitySummary>> GetDailyActivitySummary(int userId, int month);

        /// <summary>
        /// Get the list of Expenses by category name
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        Task<List<ApiActivityByCategoryKVP>> GetAllExpenseByCategory(int appUserId);
    }
}
