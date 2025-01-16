using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="Transaction"/> entity.
    /// </summary>
    public class TransactionRepo : AuditableRepo<Transaction>, ITransactionRepo
    {
        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public TransactionRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _authHelper = authHelper;
            _dbContext = dbContext;
        }

    }
}
