using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.AppUser;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="AppUser"/> entity.
    /// </summary>
    public class AppUserRepo : AuditableRepo<AppUser>, IAppUserRepo
    {
        protected readonly MyExpensesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepo"/> class.
        /// </summary>
        /// <param name="authHelperContract">The authentication helper contract used for user authentication tasks.</param>
        /// <param name="context">The database context used for interacting with the database.</param>
        public AppUserRepo(IAuthHelperContract authHelperContract, MyExpensesDbContext context) : base(context, authHelperContract)
        {
            _context = context;
        }

        /// <summary>
        /// Updates the specified <see cref="AppUser"/> entity with new values.
        /// </summary>
        /// <param name="appUser">The model containing the updated user information.</param>
        /// <param name="id">The identifier of the user to update.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        public async Task<bool> UpdateAsync(CreateAppUser appUser, int id)
        {
            var updatedCount = await _context.Set<AppUser>()
                                    .Where(u => u.Id == id)
                                    .ExecuteUpdateAsync(u => u
                                        .SetProperty(s => s.FirstName, appUser.FirstName)
                                        .SetProperty(s => s.LastName, appUser.LastName)
                                        .SetProperty(s => s.FullName, $"{appUser.FirstName} {appUser.LastName}")
                                        .SetProperty(s => s.PhoneNumber, appUser.PhoneNumber)
                                        .SetProperty(s => s.MonthlyBudget, appUser.MonthlyBudget)
                                        .SetProperty(s => s.Avatar, appUser.Avatar)
                                    );
            return updatedCount > 0;
        }
    }
}
