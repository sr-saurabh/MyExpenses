using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="GroupExpenseShare"/> entity.
    /// </summary>
    public class GroupExpenseShareRepo : AuditableRepo<GroupExpenseShare>, IGroupExpenseShareRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupExpenseShareRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public GroupExpenseShareRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
        }
    }
}
