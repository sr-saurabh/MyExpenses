using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;

namespace MyExpenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityContract _activityContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="personalExpenseContract">The contract for managing personal expenses.</param>
        public ActivityController(ITransactionContract personalExpenseContract, IActivityContract activityContract)
        {
            _activityContract= activityContract;
        }

        /// <summary>
        /// Retrieves all personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be retrieved.</param>
        /// <returns>A list of personal expenses for the specified user.</returns>
        [HttpGet("get-all-user-expense/{userId}")]
        public async Task<IActionResult> GetAllUserExpense(int userId)
        {
            var result = await _activityContract.GetActivitiesAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific personal expense by its ID.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to retrieve.</param>
        /// <returns>The details of the specified personal expense.</returns>
        [HttpGet("{expenseId}")]
        public async Task<IActionResult> GetExpense(int expenseId)
        {
            var result = await _activityContract.GetActivity(expenseId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all personal expenses for a user based on the provided filter criteria.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be filtered.</param>
        /// <param name="expenseFilter">The filter criteria to apply.</param>
        /// <returns>A list of filtered personal expenses.</returns>
        [HttpPost("get-all-filtered-expense/{userId}")]
        public async Task<IActionResult> GetAllFilteredExpense(int userId, [FromBody] ActivityFilter expenseFilter)
        {
            var result = await _activityContract.GetActivityWithSummary(userId, expenseFilter);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose expense summary is to be retrieved.</param>
        /// <returns>The expense summary for the specified user.</returns>
        [HttpGet("get-expense-summary/{userId}")]
        public async Task<IActionResult> GetExpenseSummary(int userId)
        {
            var result = await _activityContract.GetActivitySummary(userId);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new personal expense.
        /// </summary>
        /// <param name="value">The details of the expense to create.</param>
        /// <returns>A result indicating the success or failure of the expense creation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateActivity value)
        {
            var result = await _activityContract.CreateActivityAsync(value);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing personal expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to update.</param>
        /// <param name="value">The updated details of the expense.</param>
        /// <returns>A result indicating the success or failure of the expense update.</returns>
        [HttpPut("{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateActivity value)
        {
            var result = await _activityContract.UpdateActivity(value);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a personal expense by its ID.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to delete.</param>
        /// <returns>A result indicating the success or failure of the expense deletion.</returns>
        [HttpDelete("{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var result = await _activityContract.DeleteActivity(expenseId);
            return Ok(result);
        }

        /// <summary>
        /// Fetch all the categories
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>

        [HttpGet("categories/{appUserId}")]
        public async Task<IActionResult> GetCategories(int appUserId)
        {
            var res = await _activityContract.GetCategories(appUserId);
            return Ok(res);
        }

        /// <summary>
        /// Fetch all the expenses for a week
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>

        [HttpGet("weekly-summary/{appUserId}")]
        [Authorize]
        public async Task<IActionResult> GetWeeklySummary(int appUserId, bool isCurrentWeek)
        {
            var res = await _activityContract.GetWeeklyActivity(appUserId, isCurrentWeek);
            return Ok(res);
        }
        /// <summary>
        /// Fetch all the expenses for a month for a particular category
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("expense-by-category/{category}/user/{appUserId}")]
        [Authorize]
        public async Task<IActionResult> GetExpenseByCategory(int appUserId, string category)
        {
           var response = await _activityContract.GetExpenseByCategory(appUserId, category);
            return Ok(response);
        }
        
        /// <summary>
        /// Fetch all the expenses for a month for a particular category
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("all-expense-by-category/user/{appUserId}")]
        [Authorize]
        public async Task<IActionResult> GetExpenseByCategory(int appUserId)
        {
            var response = await _activityContract.GetAllExpenseByCategory(appUserId);
            return Ok(response);
        }

        /// <summary>
        /// Fetch all the expenses for a month
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("daily-activity-summary/{userId}/month/{month}")]
        [Authorize]
        public async Task<IActionResult> GetDailyActivitySummary(int userId, int month)
        {
            var response = await _activityContract.GetDailyActivitySummary(userId, month);
            return Ok(response);
        }
    }
}
