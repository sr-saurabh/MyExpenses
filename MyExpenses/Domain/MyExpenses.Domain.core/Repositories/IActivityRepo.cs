using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Repositories
{
    public interface IActivityRepo : IAuditableRepo<Activity>
    {
        /// <summary>
        /// Retrieves a collection of personal expenses filtered by the specified criteria.
        /// </summary>
        /// <param name="id">The ID of the user or entity for which expenses are filtered.</param>
        /// <param name="expression">The filter criteria applied to the expenses.</param>
        /// <returns>An IQueryable collection of filtered PersonalExpenses.</returns>
        Task<IQueryable<Activity>> GetFilteredExpenses(int id, ActivityFilter expression);

        /// <summary>
        /// Retrieves a summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the summary is retrieved.</param>
        /// <returns>A PersonalExpenseSummary object containing summarized expense data.</returns>
        Task<ActivitySummary> GetPersonalExpenseSummary(int userId);
        Task<WeeklyExpense> GetWeeklyExpense(int userId, DateTime startDate, DateTime endDate);
        Task<List<DailyActivitySummary>> GetDailyActivitySummary(int userId, DateTime startDate, DateTime endDate);
    }
}
