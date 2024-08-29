using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.GroupExpense;
using System.Threading.Tasks;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to group expenses, including creating, updating, deleting, and retrieving expenses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupExpenseController : ControllerBase
    {
        private readonly IGroupExpenseContract _groupExpenseContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupExpenseController"/> class.
        /// </summary>
        /// <param name="groupExpenseContract">The group expense contract for managing group expense operations.</param>
        public GroupExpenseController(IGroupExpenseContract groupExpenseContract)
        {
            _groupExpenseContract = groupExpenseContract;
        }

        /// <summary>
        /// Creates a new group expense.
        /// </summary>
        /// <param name="expense">The details of the group expense to create.</param>
        /// <returns>A result indicating the success or failure of the expense creation.</returns>
        [HttpPost("create-expense")]
        [Authorize]
        public async Task<IActionResult> CreateExpense([FromBody] CreateGroupExpense expense)
        {
            var result = await _groupExpenseContract.AddGroupExpense(expense);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing group expense.
        /// </summary>
        /// <param name="expense">The updated details of the group expense.</param>
        /// <returns>A result indicating the success or failure of the expense update.</returns>
        [HttpPost("update-expense")]
        [Authorize]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateGroupExpense expense)
        {
            var result = await _groupExpenseContract.UpdateGroupExpense(expense);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a group expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to delete.</param>
        /// <returns>A result indicating the success or failure of the expense deletion.</returns>
        [HttpPost("delete-expense")]
        [Authorize]
        public async Task<IActionResult> DeleteExpense([FromQuery] int expenseId)
        {
            var result = await _groupExpenseContract.DeleteGroupExpense(expenseId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all expenses for a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose expenses are to be retrieved.</param>
        /// <returns>A list of expenses for the specified group.</returns>
        [HttpGet("get-expenses")]
        [Authorize]
        public async Task<IActionResult> GetExpenses([FromQuery] int groupId)
        {
            var result = await _groupExpenseContract.GetGroupExpenses(groupId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves details of a specific group expense by its ID.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to retrieve.</param>
        /// <returns>The details of the specified expense.</returns>
        [HttpGet("get-expense")]
        [Authorize]
        public async Task<IActionResult> GetExpense([FromQuery] int expenseId)
        {
            var result = await _groupExpenseContract.GetGroupExpense(expenseId);
            return Ok(result);
        }
    }
}
