using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    public class ActivityProvider : IActivityContract
    {
        private readonly IActivityRepo _activityRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IGoalRepo _goalRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public ActivityProvider(IActivityRepo activityRepo, IMapper mapper, IGoalRepo goalRepo, IAccountRepo accountRepo, ITransactionRepo transactionRepo)
        {
            _activityRepo = activityRepo;
            _mapper = mapper;
            _goalRepo = goalRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }


        /// <summary>
        /// Create the personal expense 
        /// </summary>
        /// <param name="createActivity"></param>
        /// <returns></returns>
        public async Task<CreateActivity> CreateActivityAsync(CreateActivity createActivity)
        {
            //creating transaction
            Transaction transaction = new()
            {
                AccountId = createActivity.AccountId,
                Amount = createActivity.Amount,
                Date = createActivity.Date,
                TransactionType = createActivity.Type,
            };
            var transactionResponse = await _transactionRepo.CreateAsync(transaction);

            //creating Activity

            var activity = _mapper.Map<Activity>(createActivity);
            activity.TransactionId = transactionResponse.Id;
            var result = await _activityRepo.CreateAsync(activity);


            //adding the amount to goal
            var amount = createActivity.Amount;
            if (createActivity.Type == TransactionType.Credit)
                amount = -amount;

            var goal = _goalRepo.Search(c => c.CategoryId == createActivity.CategoryId && c.Year == createActivity.Date.Year && c.Month == createActivity.Date.Month).SingleOrDefault();
            if (goal == null)
            {
                goal = new()
                {
                    AppUserId = createActivity.AppUserId,
                    CategoryId = createActivity.CategoryId,
                    Year = createActivity.Date.Year,
                    Month = createActivity.Date.Month,
                    Budget = 0,
                    TotalSpent = amount
                };
                await _goalRepo.CreateAsync(goal);
            }
            else
            {
                var isCategoryUpdated = await _goalRepo.UpdateTotalSpentAsync(goal.Id, (decimal)(goal.TotalSpent + amount));
            }
            var accountDetail = await _accountRepo.GetByIdAsync(createActivity.AccountId);
            var isAccountUpdated = await _accountRepo.UpdateBalanceAsync(createActivity.AccountId, accountDetail.Balance - amount);


            //finally: updating the transaction with the activity Id
            var isTransactionUpdated = _transactionRepo.UpdateAsync(t => t.Id == transactionResponse.Id, t => t.SetProperty(t => t.ActivityId, result.Id));
            return _mapper.Map<CreateActivity>(createActivity);

        }


        /// <summary>
        /// Get the personal expense based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiActivity> GetActivity(int id)
        {
            var personalExpense = await _activityRepo.GetByIdAsync(id);
            return _mapper.Map<ApiActivity>(personalExpense);
        }


        /// <summary>
        /// Get all the personal expenses of a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ApiActivity>> GetActivitiesAsync(int userId)
        {

            var personalExpenses = _activityRepo.Search(pe => pe.AppUserId == userId).Include(a=>a.Transaction).ToList();
            return _mapper.Map<List<ApiActivity>>(personalExpenses);
        }

        /// <summary>
        /// Get the personal expenses based on the filter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <returns> </returns>
        public async Task<ApiActivityWithSummary> GetActivityWithSummary(int userId, ActivityFilter filter)
        {
            var result = (await _activityRepo.GetFilteredExpenses(userId, filter)).ToList();
            //var summary =await _personalExpenseRepo.GetPersonalExpenseSummary(userId, predicate);
            ActivitySummary summary = new ActivitySummary()
            {
                TotalSpent = result.Where(e => e.Transaction.TransactionType == TransactionType.Debit).Sum(e => e.Transaction.Amount),
                TotalEarning = result.Where(e => e.Transaction.TransactionType == TransactionType.Credit).Sum(e => e.Transaction.Amount)
            };
            var apiPersonalExpenses = _mapper.Map<List<ApiActivity>>(result);

            var apiPersonalExpenseWithSummary = new ApiActivityWithSummary
            {
                Expenses = apiPersonalExpenses.ToList(),
                Summary = summary
            };
            return apiPersonalExpenseWithSummary;
        }


        /// <summary>
        /// Get the summary of the personal expenses of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ActivitySummary> GetActivitySummary(int userId)
        {
            var result = await _activityRepo.GetPersonalExpenseSummary(userId);
            return result;
        }


        /// <summary>
        /// Update the existing personal expense
        /// </summary>
        /// <param name="expense"></param>
        /// <returns>Return true if updated successfully</returns>
        public async Task<bool> UpdateActivity(UpdateActivity expense)
        {
            var previousExpense = _activityRepo.Search(pe => pe.Id == expense.Id).Include(a => a.Transaction).SingleOrDefault();
            var previousAmount = previousExpense.Transaction.Amount;

            var previousAccount = await _accountRepo.GetByIdAsync((int)previousExpense.Transaction.AccountId);

            if (previousExpense.Transaction.TransactionType == TransactionType.Credit)
                previousAmount = -previousAmount;

            //updating the activity
            var isUpdated = await _activityRepo.UpdateAsync(pe => pe.Id == expense.Id, pe => pe.SetProperty(p => p.Category, expense.Category).SetProperty(p => p.Description, expense.Description).SetProperty(p => p.CategoryId, expense.CategoryId));

            //updating the Transaction
            var res = await _transactionRepo.UpdateAsync(t => t.Id == previousExpense.TransactionId, t => t.SetProperty(t => t.Amount, expense.Amount).SetProperty(p => p.Date, expense.Date).SetProperty(p => p.Date, expense.Date).SetProperty(p => p.AccountId, expense.AccountId));

            //
            // Updating the goal
            //
            var currentAmount = expense.Amount;
            if (expense.Type == TransactionType.Credit)
                currentAmount = -currentAmount;



            //if the account got changed
            if (previousExpense.Transaction.AccountId != expense.AccountId)
            {
                var currentAccount = await _accountRepo.GetByIdAsync(expense.AccountId);
                await _accountRepo.UpdateBalanceAsync((int)previousExpense.Transaction.AccountId, previousAccount.Balance + previousAmount);

                await _accountRepo.UpdateBalanceAsync((int)expense.AccountId, currentAccount.Balance - currentAmount);
            }
            //if the account for both the expenses remain same
            else
            {
                await _accountRepo.UpdateBalanceAsync((int)previousExpense.Transaction.AccountId, previousAccount.Balance + previousAmount - currentAmount);
            }


            //if expense category got changed then update the goal
            if (expense.CategoryId != previousExpense.CategoryId)
            {
                var previousGoal = _goalRepo.Search(g => g.CategoryId == previousExpense.CategoryId && g.Month == previousExpense.Transaction.Date.Month && g.Year == previousExpense.Transaction.Date.Year).SingleOrDefault();

                var isPreviousCategoryUpdated = await _goalRepo.UpdateTotalSpentAsync(previousGoal.Id, (decimal)(previousGoal.TotalSpent - previousAmount));

                var newGoal = _goalRepo.Search(g => g.CategoryId == expense.CategoryId && g.Month == expense.Date.Month && g.Year == expense.Date.Year).SingleOrDefault();

                if (newGoal == null)
                {
                    newGoal = new()
                    {
                        CategoryId = expense.CategoryId,
                        Year = expense.Date.Year,
                        Month = expense.Date.Month,
                        Budget = 0,
                        TotalSpent = currentAmount
                    };
                    await _goalRepo.CreateAsync(newGoal);
                }
                else
                {
                    var isGoalUpdated = await _goalRepo.UpdateTotalSpentAsync(newGoal.Id, (decimal)(newGoal.TotalSpent + currentAmount));
                }

            }
            else
            {
                var goal = _goalRepo.Search(g => g.CategoryId == expense.CategoryId && g.Month == expense.Date.Month && g.Year == expense.Date.Year).SingleOrDefault();


                var isCategoryUpdated = await _goalRepo.UpdateTotalSpentAsync(goal.Id, (decimal)(goal.TotalSpent - previousAmount + currentAmount));

            }
            return isUpdated;
        }

        /// <summary>
        /// Delete the personal expense
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Return true if Deleted successfully </returns>
        public async Task<bool> DeleteActivity(int activityId)
        {
            var previousExpense = _activityRepo.Search(pe => pe.Id == activityId).Include(a => a.Transaction).SingleOrDefault();
            var previousAmount = previousExpense.Transaction.Amount;
            if (previousExpense.Transaction.TransactionType == TransactionType.Credit)
                previousAmount = -previousAmount;

            var goal = _goalRepo.Search(c => c.Id == previousExpense.CategoryId && c.Month == previousExpense.Transaction.Date.Month && c.Year == previousExpense.Transaction.Date.Year).SingleOrDefault();

            var isDeleted = await _activityRepo.DeleteAsync(activityId);

            //deleting the transaction
            var transactionDeleted= await _transactionRepo.DeleteAsync(previousExpense.TransactionId);


            var isCategoryUpdated = await _goalRepo.UpdateTotalSpentAsync(goal.Id, (decimal)(goal.TotalSpent - previousAmount));

            var accountDetail = await _accountRepo.GetByIdAsync(previousExpense.Transaction.AccountId);
            var isAccountUpdated = await _accountRepo.UpdateBalanceAsync((int)previousExpense.Transaction.TransactionType, accountDetail.Balance + previousAmount);

            return isDeleted != null;
        }
        /// <summary>
        /// Get the list of categories
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns> Return the list of categories </returns>
        public async Task<List<string>> GetCategories(int appUserId)
        {
            var categories = _activityRepo.Search(pe => pe.AppUserId == appUserId).Select(pe => pe.Category).Distinct().ToList();
            return categories;
        }

        /// <summary>
        /// Get the list of categories
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns> Return the weekly expense</returns>
        public async Task<WeeklyExpense> GetWeeklyActivity(int appUserId, bool isCurrent)
        {
            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow;
            if (isCurrent)
                startDate = startDate.AddDays(-6);
            else
            {
                startDate = startDate.AddDays(-13);
                endDate = endDate.AddDays(-7);
            }

            return await _activityRepo.GetWeeklyExpense(appUserId, startDate, endDate);
        }
    }
}
