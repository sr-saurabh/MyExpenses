using MyExpenses.Domain.core.Entities.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for helper methods related to user authentication and information retrieval.
    /// </summary>
    public interface IAuthHelperContract
    {
        /// <summary>
        /// Retrieves the user's ID from the given <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="userClaimsPrincipal">The claims principal containing the user's claims.</param>
        /// <returns>The user's ID as a <see cref="Guid"/>.</returns>
        Guid GetUserId(ClaimsPrincipal? userClaimsPrincipal);

        /// <summary>
        /// Retrieves the email address of the currently authenticated user.
        /// </summary>
        /// <returns>The user's email address, or <c>null</c> if not available.</returns>
        string? GetUserEmail();

        /// <summary>
        /// Retrieves the ID of the currently authenticated user.
        /// </summary>
        /// <returns>The user's ID as a <see cref="Guid"/>.</returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// Retrieves the <see cref="AppIdentityUser"/> object for the currently authenticated user.
        /// </summary>
        /// <returns>The <see cref="AppIdentityUser"/> object, or <c>null</c> if the user is not found.</returns>
        Task<AppIdentityUser?> GetCurrentUser();
    }
}
