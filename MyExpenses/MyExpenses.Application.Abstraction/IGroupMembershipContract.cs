using MyExpenses.Domain.core.Models.AppUser;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Models.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing group memberships.
    /// </summary>
    public interface IGroupMembershipContract
    {
        /// <summary>
        /// Retrieves a list of members for a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose members are to be retrieved.</param>
        /// <returns>A list of <see cref="ApiAppUser"/> objects representing the members of the group.</returns>
        Task<List<MyContact>> GetMembersOfGroup(int groupId);

        /// <summary>
        /// Retrieves a list of groups to which a specific user belongs.
        /// </summary>
        /// <param name="userId">The ID of the user whose groups are to be retrieved.</param>
        /// <returns>A list of <see cref="ApiUserGroup"/> objects representing the groups the user is part of.</returns>
        Task<List<ApiUserGroup>> GetGroupsOfUser(int userId);

        /// <summary>
        /// Adds a user to a specific group.
        /// </summary>
        /// <param name="userId">The ID of the user to add to the group.</param>
        /// <param name="groupId">The ID of the group to which the user is to be added.</param>
        /// <returns><c>true</c> if the user was successfully added to the group; otherwise, <c>false</c>.</returns>
        Task<bool> AddUserToGroup(int userId, int groupId);

        /// <summary>
        /// Removes a user from a specific group.
        /// </summary>
        /// <param name="userId">The ID of the user to remove from the group.</param>
        /// <param name="groupId">The ID of the group from which the user is to be removed.</param>
        /// <returns><c>true</c> if the user was successfully removed from the group; otherwise, <c>false</c>.</returns>
        Task<bool> RemoveUserFromGroup(int userId, int groupId);
    }
}
