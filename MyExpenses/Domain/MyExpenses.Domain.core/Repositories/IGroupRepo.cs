using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages UserGroup entities, 
    /// extending the generic auditable repository interface.
    /// </summary>
    public interface IGroupRepo : IAuditableRepo<UserGroup>
    {
        // Additional methods specific to UserGroup repository can be added here
    }
}

