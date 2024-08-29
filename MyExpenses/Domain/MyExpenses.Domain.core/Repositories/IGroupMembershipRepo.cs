using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages UserGroupMembership entities, 
    /// extending the generic auditable repository interface.
    /// </summary>
    public interface IGroupMembershipRepo : IAuditableRepo<UserGroupMembership>
    {
        /// <summary>
        /// Retrieves all users who are members of a specified group asynchronously.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <returns>A list of AppUser entities representing the users in the group.</returns>
        Task<List<AppUser>> GetAllUsersOfGroupAsync(int groupId);

        /// <summary>
        /// Retrieves all groups that a specified user is a member of asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of UserGroup entities representing the groups the user is part of.</returns>
        Task<List<UserGroup>> GetAllGroupsOfUserAsync(int userId);

        /// <summary>
        /// Retrieves all users who are members of a specified group with their balances asynchronously.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns> A list of UserGroup entities representing the groups the user is part of.</returns>
        Task<List<MyContact>> GetAllUsersOGroupWithBalanceAsync(int groupId, int userId);
    }
}
