using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    public interface IGoalRepo : IAuditableRepo<Goal>
    {
        Task<GoalSummary> GetGoalSummary(int userId, int month, int year);
        Task<List<GoalExpenseSummary>> GetGoalExpenseSummary(int userId);
        Task<List<Goal>> GetAll(int userId, int month, int year);
        Task<bool> UpdateBudgetAsync(int id, decimal amount);
        Task<bool> UpdateTotalSpentAsync(int id, decimal amount);
        
    }
}
