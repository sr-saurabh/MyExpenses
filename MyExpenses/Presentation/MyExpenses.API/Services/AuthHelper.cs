using Microsoft.AspNetCore.Identity;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.User;
using System.Security.Claims;

namespace MyExpenses.API.Services
{
    /// <summary>
    /// Provides helper methods for managing user authentication and retrieving user information.
    /// </summary>
    public class AuthHelper : IAuthHelperContract
    {
        private string UserId { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<AppIdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthHelper"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor for the current HTTP context.</param>
        /// <param name="userManager">Manager for user-related operations.</param>
        public AuthHelper(IHttpContextAccessor httpContextAccessor, UserManager<AppIdentityUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        /// <summary>
        /// Retrieves the user's ID from the given <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="userClaimsPrincipal">The claims principal containing the user's claims.</param>
        /// <returns>The user's ID as a <see cref="Guid"/>.</returns>
        public Guid GetUserId(ClaimsPrincipal? userClaimsPrincipal)
        {
            var id = userClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return new Guid(id);
        }

        /// <summary>
        /// Retrieves the email address of the currently authenticated user.
        /// </summary>
        /// <returns>The user's email address, or <c>null</c> if not available.</returns>
        public string? GetUserEmail()
        {
            var email = httpContextAccessor.HttpContext.User.Identity.Name;
            return email;
        }

        /// <summary>
        /// Retrieves the ID of the currently authenticated user.
        /// </summary>
        /// <returns>The user's ID as a <see cref="Guid"/>.</returns>
        public Guid GetCurrentUserId()
        {
            UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return new Guid(UserId);
        }

        /// <summary>
        /// Retrieves the <see cref="AppIdentityUser"/> object for the currently authenticated user.
        /// </summary>
        /// <returns>The <see cref="AppIdentityUser"/> object, or <c>null</c> if the user is not found.</returns>
        public async Task<AppIdentityUser?> GetCurrentUser()
        {
            UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(UserId);
            return user;
        }
    }
}
