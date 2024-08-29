using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="UserGroup"/> entity.
    /// </summary>
    public class GroupRepo : AuditableRepo<UserGroup>, IGroupRepo
    {
        private readonly MyExpensesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public GroupRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _context = dbContext;
        }
    }
}
