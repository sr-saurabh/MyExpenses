using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;

namespace MyExpenses.Application.Abstraction
{
    public interface IGoalContract
    {
        Task<List<ApiGoal>> GetAll(int userId, int month, int year);
        Task<GoalSummary> GetGoalSummary(int userId, int month, int year);
        Task<ApiGoal> CreateGoal(CreateGoal goal);
        Task<bool> UpdateGoal(int userId, UpdateGoal goal);
        Task<List<GoalExpenseSummary>> GetGoalExpenseSummary(int userId);

    }
}
