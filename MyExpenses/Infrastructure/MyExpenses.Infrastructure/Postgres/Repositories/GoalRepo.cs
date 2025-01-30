using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class GoalRepo : AuditableRepo<Goal>, IGoalRepo
    {
        private readonly MyExpensesDbContext dbContext;
        public GoalRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            this.dbContext = dbContext;
        }

        public async Task<GoalSummary> GetGoalSummary(int userId, int month, int year)
        {
            var categories = await dbContext.Goals
           .Where(c => c.AppUserId == userId && c.Month == month && c.Year == year).ToListAsync();

            var totalTarget = categories.Sum(c => c.Budget ?? 0);
            var targetAchieved = categories.Sum(c => c.TotalSpent ?? 0);

            // Create the goal summary object
            return new GoalSummary
            {
                Budget = totalTarget,
                TargetSpent = targetAchieved,
                Month = month,
                Year = year
            };
        }

        public async Task<bool> UpdateTotalSpentAsync(int id, decimal amount)
        {
            var updatedCount = await _dbContext.Set<Goal>()
                                    .Where(c => c.Id == id)
                                    .ExecuteUpdateAsync(g =>
                                        g.SetProperty(g => g.TotalSpent, amount)
                                     );

            return updatedCount > 0;
        }
        public async Task<bool> UpdateBudgetAsync(int id, decimal amount)
        {
            var updatedCount = await _dbContext.Set<Goal>()
                                    .Where(c => c.Id == id)
                                    .ExecuteUpdateAsync(g =>
                                        g.SetProperty(g => g.Budget, amount)
                                    );

            return updatedCount > 0;
        }

        public async Task<List<Goal>> GetAll(int userId, int month, int year)
        {
            var goals =await Search(g => g.AppUserId == userId && g.Month == month && g.Year == year).Include(g => g.Category).ToListAsync();
            return goals;
        }

        public async Task<List<GoalExpenseSummary>> GetGoalExpenseSummary(int userId)
        {
            var month= DateTime.UtcNow.Month;
            var year= DateTime.UtcNow.Year;
            int previousMonth = month - 1;
            int previousMonthYear = year;

            if (previousMonth == 0) // Handle January case
            {
                previousMonth = 12;
                previousMonthYear = year - 1;
            }
            var summaries = await _dbContext.Goals.Where(g => g.AppUserId == userId)
                .GroupBy(goal => new { goal.CategoryId, goal.Category.CategoryName })
                .Select(g => new GoalExpenseSummary
                {
                    Id = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    CurrentMonthExpense = g.Where(goal => goal.Year == year && goal.Month == month).Sum(goal => goal.TotalSpent) ?? 0,
                    PreviousMonthExpense = g.Where(goal => goal.Year == previousMonthYear && goal.Month == previousMonth).Sum(goal => goal.TotalSpent) ?? 0
                })
                .ToListAsync();
            return summaries;
        }
    }
}
