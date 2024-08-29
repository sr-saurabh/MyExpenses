using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Contact;
using System.Threading.Tasks;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to contacts, including adding, accepting, rejecting, removing, and retrieving contacts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactContract _contactContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// </summary>
        /// <param name="contactContract">The contact contract for managing contact operations.</param>
        public ContactController(IContactContract contactContract)
        {
            _contactContract = contactContract;
        }

        /// <summary>
        /// Adds a contact request between two users.
        /// </summary>
        /// <param name="fromUserId">The ID of the user sending the contact request.</param>
        /// <param name="toUserId">The ID of the user receiving the contact request.</param>
        /// <returns>A result indicating the success or failure of the contact request addition.</returns>
        [HttpPost]
        [Route("add-contact")]
        [Authorize]
        public async Task<IActionResult> AddContact(int fromUserId, int toUserId)
        {
            var result = await _contactContract.AddContact(fromUserId, toUserId);
            return Ok(result);
        }

        /// <summary>
        /// Accepts a contact request.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to accept.</param>
        /// <returns>A result indicating the success or failure of the contact acceptance.</returns>
        [HttpPost]
        [Route("accept-contact")]
        [Authorize]
        public async Task<IActionResult> AcceptContact(int contactId)
        {
            var result = await _contactContract.AcceptContact(contactId);
            return Ok(result);
        }

        /// <summary>
        /// Rejects a contact request.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to reject.</param>
        /// <returns>A result indicating the success or failure of the contact rejection.</returns>
        [HttpPost]
        [Route("reject-contact")]
        [Authorize]
        public async Task<IActionResult> RejectContact(int contactId)
        {
            var result = await _contactContract.RejectContact(contactId);
            return Ok(result);
        }

        /// <summary>
        /// Removes a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact to remove.</param>
        /// <returns>A result indicating the success or failure of the contact removal.</returns>
        [HttpDelete]
        [Route("remove-contact")]
        [Authorize]
        public async Task<IActionResult> RemoveContact(int contactId)
        {
            var result = await _contactContract.RemoveContact(contactId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of contacts for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contacts are to be retrieved.</param>
        /// <returns>A list of contacts for the specified user.</returns>
        [HttpGet]
        [Route("get-contacts")]
        [Authorize]
        public async Task<IActionResult> GetContacts(int userId)
        {
            var result = await _contactContract.GetContacts(userId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves contact requests for a specific user.
        /// </summary>
        /// <param name="toUserId">The ID of the user who is receiving contact requests.</param>
        /// <returns>A list of contact requests for the specified user.</returns>
        [HttpGet]
        [Route("get-contact-request")]
        [Authorize]
        public async Task<IActionResult> GetContactRequest(int toUserId)
        {
            var result = await _contactContract.GetContactRequest(toUserId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific contact by its ID. (Not yet implemented)
        /// </summary>
        /// <param name="contactId">The ID of the contact to retrieve.</param>
        /// <returns>A result indicating the success or failure of the contact retrieval.</returns>
        [HttpGet]
        [Route("get-contact")]
        [Authorize]
        public async Task<IActionResult> GetContact(int contactId)
        {
            // TODO: Implement contact retrieval
            // var result = await _contactContract.GetContact(contactId);
            return Ok(); // Placeholder response
        }
    }
}
