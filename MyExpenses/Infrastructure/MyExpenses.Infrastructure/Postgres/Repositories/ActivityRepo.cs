using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class ActivityRepo : AuditableRepo<Activity>, IActivityRepo
    {
        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _dbContext;
        public ActivityRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _authHelper = authHelper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a queryable collection of <see cref="Transaction"/> based on the provided filter.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be retrieved.</param>
        /// <param name="filter">The filter criteria to apply to the expense retrieval.</param>
        /// <returns>A queryable collection of <see cref="Transaction"/> matching the filter criteria.</returns>
        public async Task<IQueryable<Activity>> GetFilteredExpenses(int userId, PersonalExpenseFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = _dbContext.Activities.Include(a => a.Transaction).AsQueryable();

            query = query.Where(e => e.AppUserId == userId);

            if (filter.DateFilter != null && filter.DateFilter.ExpenseDate.HasValue)
            {
                query = query.Where(e => e.Transaction.Date.Date == filter.DateFilter.ExpenseDate.Value.Date);
            }
            else if (filter.DateFilter != null && filter.DateFilter.StartDate.HasValue && filter.DateFilter.EndDate.HasValue)
            {
                query = query.Where(e => e.Transaction.Date.Date >= filter.DateFilter.StartDate.Value.Date && e.Transaction.Date.Date <= filter.DateFilter.EndDate.Value.Date);
            }

            if (filter.Categories != null && filter.Categories.Any())
            {
                query = query.Where(e => filter.Categories.Contains(e.Category));
            }

            if (filter.Type != null && filter.Type.HasValue)
            {
                query = query.Where(e => e.Transaction.TransactionType == filter.Type.Value);
            }

            if (filter.AmountFilter != null && filter.AmountFilter.Amount.HasValue)
            {
                query = query.Where(e => e.Transaction.Amount == filter.AmountFilter.Amount.Value);
            }
            else if (filter.AmountFilter != null && filter.AmountFilter.MinAmount.HasValue && filter.AmountFilter.MaxAmount.HasValue)
            {
                query = query.Where(e => e.Transaction.Amount >= filter.AmountFilter.MinAmount.Value && e.Transaction.Amount <= filter.AmountFilter.MaxAmount.Value);
            }

            if (filter.Month != null && filter.Year != null && filter.Month.HasValue && filter.Year.HasValue)
            {
                query = query.Where(e => e.Transaction.Date.Month == filter.Month.Value && e.Transaction.Date.Year == filter.Year.Value);
            }

            return query;
        }

        /// <summary>
        /// Retrieves the summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the expense summary is to be retrieved.</param>
        /// <returns>A <see cref="ActivitySummary"/> object containing the total spent and total earning amounts.</returns>
        public async Task<ActivitySummary> GetPersonalExpenseSummary(int userId)
        {
            try
            {
                decimal totalSpent = 0;
                decimal totalEarning = 0;

                var expenses = _dbContext.Activities.Where(pe => pe.AppUserId == userId).Include(a => a.Transaction);
                totalSpent = expenses.Where(e => e.Transaction.TransactionType == TransactionType.Debit).Sum(e => e.Transaction.Amount);
                totalEarning = expenses.Where(e => e.Transaction.TransactionType == TransactionType.Credit).Sum(e => e.Transaction.Amount);

                return new ActivitySummary
                {
                    TotalSpent = totalSpent,
                    TotalEarning = totalEarning
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new InvalidOperationException("Error calculating personal expense summary.", ex);
            }
        }

        public async Task<WeeklyExpense> GetWeeklyExpense(int userId, DateTime startDate, DateTime endDate)
        {
            var filteredTransactions = _dbContext.Activities.Include(a => a.Transaction)
                .Where(t => t.AppUserId == userId
                            && t.Transaction.Date >= startDate
                            && t.Transaction.Date <= endDate
                            && t.Transaction.TransactionType == TransactionType.Debit)

                .ToList();
            var dailyTotals = filteredTransactions
                                .GroupBy(t => t.Transaction.Date.Date)
                                .OrderBy(g => g.Key) // Ensure days are sorted by actual date
                                .Select(g => new
                                {
                                    Date = g.Key,
                                    Total = g.Sum(t => t.Transaction.Amount)
                                })
                                .ToList();
            var expenses = Enumerable.Range(0, 7)
                    .Select(offset =>
                    {
                        var date = startDate.AddDays(offset).Date;
                        var dayExpense = dailyTotals.FirstOrDefault(d => d.Date == date);

                        return dayExpense?.Total ?? 0; // Default to 0 if no transactions for that day
                    })
                    .ToList();

            return new WeeklyExpense
            {
                Expenses = expenses,
                StartDay = DateOnly.FromDateTime(startDate),
                EndDay = DateOnly.FromDateTime(endDate)
            };
        }
    }
}
