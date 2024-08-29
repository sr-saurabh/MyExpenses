using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to personal expenses, including retrieval, creation, updating, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalExpenseController : ControllerBase
    {
        private readonly IPersonalExpenseContract _personalExpenseContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalExpenseController"/> class.
        /// </summary>
        /// <param name="personalExpenseContract">The contract for managing personal expenses.</param>
        public PersonalExpenseController(IPersonalExpenseContract personalExpenseContract)
        {
            _personalExpenseContract = personalExpenseContract;
        }

        /// <summary>
        /// Retrieves all personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be retrieved.</param>
        /// <returns>A list of personal expenses for the specified user.</returns>
        [HttpGet("get-all-user-expense/{userId}")]
        public async Task<IActionResult> GetAllUserExpense(int userId)
        {
            var result = await _personalExpenseContract.GetPersonalExpenses(userId);
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
            var result = await _personalExpenseContract.GetPersonalExpense(expenseId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all personal expenses for a user based on the provided filter criteria.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be filtered.</param>
        /// <param name="expenseFilter">The filter criteria to apply.</param>
        /// <returns>A list of filtered personal expenses.</returns>
        [HttpPost("get-all-filtered-expense/{userId}")]
        public async Task<IActionResult> GetAllFilteredExpense(int userId, [FromBody] PersonalExpenseFilter expenseFilter)
        {
            var result = await _personalExpenseContract.GetPersonalExpenses(userId, expenseFilter);
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
            var result = await _personalExpenseContract.GetPersonalExpenseSummary(userId);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new personal expense.
        /// </summary>
        /// <param name="value">The details of the expense to create.</param>
        /// <returns>A result indicating the success or failure of the expense creation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreatePersonalExpense value)
        {
            var result = await _personalExpenseContract.CreatePersonalExpenses(value);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing personal expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to update.</param>
        /// <param name="value">The updated details of the expense.</param>
        /// <returns>A result indicating the success or failure of the expense update.</returns>
        [HttpPut("{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdatePersonalExpense value)
        {
            var result = await _personalExpenseContract.UpdatePersonalExpense(value);
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
            var result = await _personalExpenseContract.DeletePersonalExpense(expenseId);
            return Ok(result);
        }
    }
}
