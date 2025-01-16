using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class AccountRepo : AuditableRepo<Account>, IAccountRepo
    {

        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepo"/> class.
        /// </summary>
        /// <param name="authHelperContract">The authentication helper contract used for user authentication tasks.</param>
        /// <param name="context">The database context used for interacting with the database.</param>
        public AccountRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _context = dbContext;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Updates the specified <see cref="Account"/> entity with new values.
        /// </summary>
        /// <param name="account">The model containing the updated user information.</param>
        /// <param name="id">The identifier of the user to update.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        public async Task<bool> UpdateBankDetailsAsync(Account account)
        {
            var updatedCount = await _context.Set<Account>()
                                    .Where(u => u.Id == account.Id)
                                    .ExecuteUpdateAsync(u => u
                                        .SetProperty(s => s.AccountName, account.AccountName)
                                        .SetProperty(s => s.AccountNumber, account.AccountNumber)
                                        .SetProperty(s => s.AccountType, account.AccountType)
                                        .SetProperty(s => s.BranchName, account.BranchName)
                                        .SetProperty(s => s.IFSC, account.IFSC)
                                    );
            return updatedCount > 0;
        }
        public async Task<bool> UpdateBalanceAsync(int accountId, decimal balance)
        {
            var updatedCount = await _context.Set<Account>()
                                    .Where(u => u.Id == accountId)
                                    .ExecuteUpdateAsync(u => u
                                        .SetProperty(s => s.Balance, balance)
                                    );
            return updatedCount > 0;
        }
    }
}
