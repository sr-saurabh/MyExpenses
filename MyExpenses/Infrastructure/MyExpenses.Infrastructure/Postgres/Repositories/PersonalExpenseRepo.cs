using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="PersonalExpenses"/> entity.
    /// </summary>
    public class PersonalExpenseRepo : AuditableRepo<PersonalExpenses>, IPersonalExpenseRepo
    {
        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalExpenseRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public PersonalExpenseRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _authHelper = authHelper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a queryable collection of <see cref="PersonalExpenses"/> based on the provided filter.
        /// </summary>
        /// <param name="userId">The ID of the user whose expenses are to be retrieved.</param>
        /// <param name="filter">The filter criteria to apply to the expense retrieval.</param>
        /// <returns>A queryable collection of <see cref="PersonalExpenses"/> matching the filter criteria.</returns>
        public async Task<IQueryable<PersonalExpenses>> GetFilteredExpenses(int userId, PersonalExpenseFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = _dbContext.PersonalExpenses.AsQueryable();

            query = query.Where(e => e.AppUserId == userId);

            if (filter.DateFilter != null && filter.DateFilter.ExpenseDate.HasValue)
            {
                query = query.Where(e => e.Date.Date == filter.DateFilter.ExpenseDate.Value.Date);
            }
            else if (filter.DateFilter != null && filter.DateFilter.StartDate.HasValue && filter.DateFilter.EndDate.HasValue)
            {
                query = query.Where(e => e.Date.Date >= filter.DateFilter.StartDate.Value.Date && e.Date.Date <= filter.DateFilter.EndDate.Value.Date);
            }

            if (filter.Categories != null && filter.Categories.Any())
            {
                query = query.Where(e => filter.Categories.Contains(e.Category));
            }

            if (filter.Type != null && filter.Type.HasValue)
            {
                query = query.Where(e => e.Type == filter.Type.Value);
            }

            if (filter.AmountFilter != null && filter.AmountFilter.Amount.HasValue)
            {
                query = query.Where(e => e.Amount == filter.AmountFilter.Amount.Value);
            }
            else if (filter.AmountFilter != null && filter.AmountFilter.MinAmount.HasValue && filter.AmountFilter.MaxAmount.HasValue)
            {
                query = query.Where(e => e.Amount >= filter.AmountFilter.MinAmount.Value && e.Amount <= filter.AmountFilter.MaxAmount.Value);
            }

            if (filter.Month != null && filter.Year != null && filter.Month.HasValue && filter.Year.HasValue)
            {
                query = query.Where(e => e.Date.Month == filter.Month.Value && e.Date.Year == filter.Year.Value);
            }

            return query;
        }

        /// <summary>
        /// Retrieves the summary of personal expenses for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the expense summary is to be retrieved.</param>
        /// <returns>A <see cref="PersonalExpenseSummary"/> object containing the total spent and total earning amounts.</returns>
        public async Task<PersonalExpenseSummary> GetPersonalExpenseSummary(int userId)
        {
            try
            {
                decimal totalSpent = 0;
                decimal totalEarning = 0;

                var expenses = _dbContext.PersonalExpenses.Where(pe => pe.AppUserId == userId);
                totalSpent = expenses.Where(e => e.Type == TransactionType.Debit).Sum(e => e.Amount);
                totalEarning = expenses.Where(e => e.Type == TransactionType.Credit).Sum(e => e.Amount);

                return new PersonalExpenseSummary
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
    }
}
