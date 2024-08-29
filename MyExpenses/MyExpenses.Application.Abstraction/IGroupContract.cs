using MyExpenses.Domain.core.Models.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing user groups.
    /// </summary>
    public interface IGroupContract
    {
        /// <summary>
        /// Retrieves a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to retrieve.</param>
        /// <returns>An <see cref="ApiUserGroup"/> object representing the group.</returns>
        Task<ApiUserGroup> GetGroupById(int id);

        /// <summary>
        /// Retrieves a list of all groups.
        /// </summary>
        /// <returns>A list of <see cref="ApiUserGroup"/> objects representing all groups.</returns>
        Task<List<ApiUserGroup>> GetGroups();

        /// <summary>
        /// Creates a new group with the specified details.
        /// </summary>
        /// <param name="group">The details of the group to create.</param>
        /// <returns>An <see cref="ApiUserGroup"/> object representing the created group.</returns>
        Task<ApiUserGroup> CreateGroup(CreateGroup group);

        /// <summary>
        /// Updates an existing group with the specified details.
        /// </summary>
        /// <param name="group">The updated group details.</param>
        /// <param name="userId">The ID of the user performing the update.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateGroup(UpdateGroup group, int userId);

        /// <summary>
        /// Deletes a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to delete.</param>
        /// <returns><c>true</c> if the group was successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteGroup(int id);
    }
}
