using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.AppUser;
using System.Security.Claims;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to application users, including registration, retrieval, updating, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserContract _appUserContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserController"/> class.
        /// </summary>
        /// <param name="appUserContract">The application user contract for managing user operations.</param>
        public AppUserController(IAppUserContract appUserContract)
        {
            _appUserContract = appUserContract;
        }

        /// <summary>
        /// Registers a new application user.
        /// </summary>
        /// <param name="user">The details of the user to register.</param>
        /// <returns>A result indicating the success or failure of the registration.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterAppUser(CreateAppUser user)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = HttpContext.User.Identity?.Name;
            user.UserId = new Guid(userId);
            var result = await _appUserContract.RegisterAppUser(user, email);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all application users.
        /// </summary>
        /// <returns>A list of application users in type <see cref="ApiAppUser"/>.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAppUsers()
        {
            var users = await _appUserContract.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific application user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The details of the specified user in type <see cref="ApiAppUser"/>.</returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAppUser(int userId)
        {
            var user = await _appUserContract.GetUser(userId);
            return Ok(user);
        }

        /// <summary>
        /// Retrieves the currently authenticated application user.
        /// </summary>
        /// <returns>The details of the currently authenticated user in type <see cref="ApiAppUser"/>.</returns>
        [HttpGet]
        [Route("get-current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentAppUser()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserId = new Guid(userId);
            var user = await _appUserContract.GetCurrentAppUser(currentUserId);

            return Ok(user);
        }

        /// <summary>
        /// Updates the details of a specific application user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="user">The updated user details.</param>
        /// <returns>A result indicating the success or failure of the update.</returns>
        [HttpPut]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAppUser(int userId, CreateAppUser user)
        {
            var result = await _appUserContract.UpdateUser(user, userId);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific application user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A result indicating the success or failure of the deletion.</returns>
        [HttpDelete]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAppUser(int userId)
        {
            var result = await _appUserContract.DeleteUser(userId);
            return Ok(result);
        }

        /// <summary>
        /// Searches for users based on the provided search criteria.
        /// </summary>
        /// <param name="user">The search criteria for finding users.</param>
        /// <returns>A list of users matching the search criteria in type <see cref="ApiAppUser"/>.</returns>
        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<IActionResult> SearchUser(SearchUser user)
        {
            var result = await _appUserContract.SearchUser(user);
            return Ok(result);
        }
    }
}
