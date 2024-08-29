using MyExpenses.Domain.core.Models.AppUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for user-related operations.
    /// </summary>
    public interface IAppUserContract
    {
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the user.</returns>
        Task<ApiAppUser> GetUser(int id);

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of <see cref="ApiAppUser"/> objects representing all users.</returns>
        Task<List<ApiAppUser>> GetUsers();

        /// <summary>
        /// Registers a new user with the specified email.
        /// </summary>
        /// <param name="user">The user details to register.</param>
        /// <param name="email">The email address to associate with the new user.</param>
        /// <returns>A <see cref="CreateAppUser"/> object representing the registered user.</returns>
        Task<CreateAppUser> RegisterAppUser(CreateAppUser user, string email);

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="user">The updated user details.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateUser(CreateAppUser user, int id);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The ID of the deleted user.</returns>
        Task<bool> DeleteUser(int id);

        /// <summary>
        /// Retrieves the currently authenticated user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the currently authenticated user.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the current user.</returns>
        Task<ApiAppUser> GetCurrentAppUser(Guid userId);

        /// <summary>
        /// Searches for a user based on the provided search criteria.
        /// </summary>
        /// <param name="user">The search criteria.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the found user.</returns>
        Task<ApiAppUser> SearchUser(SearchUser user);
    }
}
