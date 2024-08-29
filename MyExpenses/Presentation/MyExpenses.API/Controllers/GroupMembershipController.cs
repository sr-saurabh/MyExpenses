using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using System.Threading.Tasks;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to group memberships, including adding, removing, and retrieving users in groups.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembershipController : ControllerBase
    {
        private readonly IGroupMembershipContract _groupMembershipContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMembershipController"/> class.
        /// </summary>
        /// <param name="groupMembershipContract">The contract for managing group memberships.</param>
        public GroupMembershipController(IGroupMembershipContract groupMembershipContract)
        {
            _groupMembershipContract = groupMembershipContract;
        }

        /// <summary>
        /// Retrieves all users who are members of a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose members are to be retrieved.</param>
        /// <returns>A list of users in the specified group.</returns>
        [HttpGet("get-all-users/{groupId}")]
        public async Task<IActionResult> GetAllUsers(int groupId)
        {
            var result = await _groupMembershipContract.GetMembersOfGroup(groupId);
            return Ok(result);
        }

        /// <summary>
        /// Adds a user to a specific group.
        /// </summary>
        /// <param name="userId">The ID of the user to add.</param>
        /// <param name="groupId">The ID of the group to which the user will be added.</param>
        /// <returns>A result indicating the success or failure of the user addition.</returns>
        [HttpPost("add-user/{userId}/{groupId}")]
        public async Task<IActionResult> AddUserToGroup(int userId, int groupId)
        {
            var result = await _groupMembershipContract.AddUserToGroup(userId, groupId);
            return Ok(result);
        }

        /// <summary>
        /// Removes a user from a specific group.
        /// </summary>
        /// <param name="userId">The ID of the user to remove.</param>
        /// <param name="groupId">The ID of the group from which the user will be removed.</param>
        /// <returns>A result indicating the success or failure of the user removal.</returns>
        [HttpDelete("remove-user/{userId}/{groupId}")]
        public async Task<IActionResult> RemoveUserFromGroup(int userId, int groupId)
        {
            var result = await _groupMembershipContract.RemoveUserFromGroup(userId, groupId);
            return Ok(result);
        }
    }
}
