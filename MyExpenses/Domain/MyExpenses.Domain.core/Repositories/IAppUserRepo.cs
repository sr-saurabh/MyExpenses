using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages AppUser entities, extending the generic auditable repository interface.
    /// </summary>
    public interface IAppUserRepo : IAuditableRepo<AppUser>
    {
        // Example of a custom method that could be added to the interface
        // Task<bool> UpdateAsync(CreateAppUser appUser, int id);
    }
}
