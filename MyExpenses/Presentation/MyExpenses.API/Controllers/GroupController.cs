using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Group;
using System.Threading.Tasks;

namespace MyExpenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupContract _groupContract;
        private readonly IGroupMembershipContract _groupMembershipContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="groupContract">The group contract for managing group operations.</param>
        /// <param name="groupMembershipContract">The group membership contract for managing group memberships.</param>
        public GroupController(IGroupContract groupContract, IGroupMembershipContract groupMembershipContract)
        {
            _groupContract = groupContract;
            _groupMembershipContract = groupMembershipContract;
        }

        /// <summary>
        /// Retrieves a list of all groups.
        /// </summary>
        /// <returns>A list of groups.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _groupContract.GetGroups();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific group by its ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose group is to be retrieved.</param>
        /// <returns>The group details.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var result = await _groupContract.GetGroupById(userId);
            return Ok(result);
        }

        /// <summary>
        /// Returns all the groups for a particular user.
        /// </summary>
        /// <param name="userId">The ID of the user whose groups are to be retrieved.</param>
        /// <returns>A list of groups for the specified user.</returns>
        [HttpGet("user-groups/{userId}")]
        public async Task<IActionResult> GetAllUserGroups(int userId)
        {
            var result = await _groupMembershipContract.GetGroupsOfUser(userId);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="value">The details of the group to create.</param>
        /// <returns>A result indicating the success or failure of the group creation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroup value)
        {
            var result = await _groupContract.CreateGroup(value);
            return Ok(result);
        }

        /// <summary>
        /// Updates the details of an existing group.
        /// </summary>
        /// <param name="userId">The ID of the user updating the group.</param>
        /// <param name="value">The updated details of the group.</param>
        /// <returns>A result indicating the success or failure of the group update.</returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateGroup(int userId, [FromBody] UpdateGroup value)
        {
            var result = await _groupContract.UpdateGroup(value, userId);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a group by its ID.
        /// </summary>
        /// <param name="userId">The ID of the group to delete.</param>
        /// <returns>A result indicating the success or failure of the group deletion.</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _groupContract.DeleteGroup(userId);
            return Ok(result);
        }
    }
}
