using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;
using MyExpenses.Domain.core.Repositories;
using System.Data.Entity;

namespace MyExpenses.Application.Providers
{
    public class GoalProvider : IGoalContract
    {
        private readonly IGoalRepo _goalRepo;
        private readonly IMapper _mapper;

        public GoalProvider(IMapper mapper, IGoalRepo goalRepo)
        {
            _mapper = mapper;
            _goalRepo = goalRepo;
        }

        public async Task<ApiGoal> CreateGoal(CreateGoal createGoal)
        {
            var goal = _mapper.Map<Goal>(createGoal);
            var res = await _goalRepo.CreateAsync(goal);
            return _mapper.Map<ApiGoal>(res);
        }

        public async Task<List<ApiGoal>> GetAll(int userId, int month, int year)
        {
            var goals = await _goalRepo.GetAll(userId, month, year);
            return _mapper.Map<List<ApiGoal>>(goals);

        }

        public async Task<List<GoalExpenseSummary>> GetGoalExpenseSummary(int userId)
        {
            return await _goalRepo.GetGoalExpenseSummary(userId);
        }

        public async Task<GoalSummary> GetGoalSummary(int userId, int month, int year)
        {
            var summary = await _goalRepo.GetGoalSummary(userId, month, year);
            return summary;
        }

        public async Task<bool> UpdateGoal(int userId, UpdateGoal goal)
        {
            var previousGoal = _goalRepo.Search(g => g.AppUserId == userId && g.Year == goal.Year && g.Month == goal.Month && g.CategoryId==goal.CategoryId).FirstOrDefault();
            if (previousGoal==null)
            {
                var newGoal = new Goal()
                {
                    AppUserId = userId,
                    Budget = goal.Budget,
                    Month = goal.Month,
                    Year = goal.Year,
                    CategoryId = goal.CategoryId,
                    TotalSpent = 0
                };
                var res = await _goalRepo.CreateAsync(newGoal);
                return res != null;

            }
            var isUpdated = await _goalRepo.UpdateBudgetAsync(goal.Id, goal.Budget);
            return isUpdated;
        }
    }
}
