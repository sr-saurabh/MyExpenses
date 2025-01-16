using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;

namespace MyExpenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalContract _goalContract;
        public GoalController(IGoalContract goalContract)
        {
            _goalContract = goalContract;
        }

        [HttpGet("user/{userId}/month/{month}/year/{year}")]
        public async Task<IActionResult> GetAll(int userId, int month, int year)
        {
            var res = await _goalContract.GetAll(userId, month, year);
            return Ok(res);
        }


        [HttpGet("get-summary/user/{userId}/month/{month}/year/{year}")]
        public async Task<IActionResult> GetSummary(int userId, int month, int year)
        {
            var res = await _goalContract.GetGoalSummary(userId, month, year);
            return Ok(res);
        }


        [HttpPut("user/{userId}")]
        public async Task<IActionResult> UpdateBudget(int userId, [FromBody] UpdateGoal goal)
        {
            var res = await _goalContract.UpdateGoal(userId, goal);
            return Ok(res);
        }

        [HttpGet("goal-expense-summary/{appUserId}")]
        public async Task<IActionResult> GetExpenseCategorySummary(int appUserId)
        {
            var res = await _goalContract.GetGoalExpenseSummary(appUserId);
            return Ok(res);
        }
    }
}
